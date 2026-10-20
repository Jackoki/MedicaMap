using MedicaMap.DTOs;

namespace MedicaMap.Services;

public class EstablishmentAPIService
{
    private readonly BnafarAPIService _bnafarApiService;

    public EstablishmentAPIService(BnafarAPIService bnafarApiService)
    {
        _bnafarApiService = bnafarApiService;
    }
    public async Task<List<BNAFAREstoqueDTO>>ObterEstabelecimentosPorMunicipioAsync(int codigoUf, int codigoMunicipio)
    {
        var estabelecimentos = new Dictionary<int, BNAFAREstoqueDTO>();

        await foreach (var estoque in _bnafarApiService.ObterEstoquesAsync(codigoUf, codigoMunicipio))
        {
            if (!estabelecimentos.ContainsKey(estoque.CodigoCnes))
            {
                estabelecimentos.Add(estoque.CodigoCnes, estoque);
            }
        }

        return estabelecimentos.Values.ToList();
    }
}
