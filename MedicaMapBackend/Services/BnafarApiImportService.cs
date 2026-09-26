using MedicaMap.Data;
using MedicaMap.DTOs;
using MedicaMap.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicaMap.Services;

public class BnafarApiImportService
{
    private readonly MedicaMapContext _context;
    private readonly BnafarApiService _apiService;

    public BnafarApiImportService(MedicaMapContext context, BnafarApiService apiService)
    {
        _context = context;
        _apiService = apiService;
    }

    public async Task ImportarAsync()
    {
        var lote = new List<BNAFAREstoqueDTO>();

        await foreach (var dto in _apiService.ObterEstoquesAsync())
        {
            lote.Add(dto);

            if (lote.Count >= 100)
            {
                await ProcessarLoteAsync(lote);
                lote.Clear();
            }
        }

        if (lote.Count > 0)
        {
            await ProcessarLoteAsync(lote);
        }
    }

    private async Task ProcessarLoteAsync(List<BNAFAREstoqueDTO> lote)
    {
        var registrosAgrupados = lote
            .GroupBy(x => new
            {
                x.CodigoUf,
                x.CodigoMunicipio,
                x.CodigoCnes,
                x.CodigoCatmat,
                Data = x.DataPosicaoEstoque.Date
            })
            .Select(g => new RegistroAgrupado
            {
                CodigoUf = g.Key.CodigoUf,
                CodigoMunicipio = g.Key.CodigoMunicipio,
                CodigoCnes = g.Key.CodigoCnes,
                CodigoCatmat = g.Key.CodigoCatmat,
                Data = g.Key.Data,
                Quantidade = g.Sum(x => x.QuantidadeEstoque),
                TipoProduto = g.First().TipoProduto,
                DescricaoProduto = g.First().DescricaoProduto,
                NomeFantasia = g.First().NomeFantasia
            })
            .ToList();

        foreach (var registro in registrosAgrupados)
        {
            await ProcessarRegistroAsync(registro);
        }

        await _context.SaveChangesAsync();
    }

    private async Task ProcessarRegistroAsync(RegistroAgrupado registro)
    {
        var estado = await _context.States
            .FirstOrDefaultAsync(s =>
                s.IbgeCode == registro.CodigoUf.ToString());

        if (estado == null)
        {
            return;
        }

        var municipio = await _context.Municipalities.FirstOrDefaultAsync(m => m.IbgeCode == registro.CodigoMunicipio.ToString() && m.StateId == estado.Id);

        if (municipio == null)
        {
            return;
        }

        var estabelecimento = await _context.Establishments.FirstOrDefaultAsync(e => e.CnesCode == registro.CodigoCnes.ToString() && e.MunicipalityId == municipio.Id);

        if (estabelecimento == null)
        {
            estabelecimento = new Establishment
            {
                CnesCode = registro.CodigoCnes.ToString(),
                TradeName = registro.NomeFantasia,
                MunicipalityId = municipio.Id
            };

            _context.Establishments.Add(estabelecimento);

            await _context.SaveChangesAsync();
        }

        var medicamento = await _context.Medications.FirstOrDefaultAsync(m => m.CatmatCode == registro.CodigoCatmat);

        if (medicamento == null)
        {
            medicamento = new Medication
            {
                CatmatCode = registro.CodigoCatmat,
                Description = registro.DescricaoProduto ?? string.Empty,
                ProductType = registro.TipoProduto ?? string.Empty
            };

            _context.Medications.Add(medicamento);

            await _context.SaveChangesAsync();
        }

        var estoque = await _context.Stocks.FirstOrDefaultAsync(s => s.EstablishmentId == estabelecimento.Id && s.MedicationId == medicamento.Id && s.StockDate == registro.Data);

        if (estoque == null)
        {
            estoque = new Stock
            {
                EstablishmentId = estabelecimento.Id,
                MedicationId = medicamento.Id,
                StockDate = registro.Data,
                Quantity = registro.Quantidade
            };

            _context.Stocks.Add(estoque);
        }
        else
        {
            estoque.Quantity = registro.Quantidade;
        }
    }

    public async Task TestarImportacaoAsync()
    {
        var registros = await _apiService.ObterPrimeirosRegistrosAsync();

        if (registros.Count == 0)
        {
            return;
        }

        await ProcessarLoteAsync(registros);
    }

    private class RegistroAgrupado
    {
        public int CodigoUf { get; set; }
        public int CodigoMunicipio { get; set; }
        public int CodigoCnes { get; set; }
        public string CodigoCatmat { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public decimal Quantidade { get; set; }
        public string? TipoProduto { get; set; }
        public string? DescricaoProduto { get; set; }
        public string? NomeFantasia { get; set; }
    }
}