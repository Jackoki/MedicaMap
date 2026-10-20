
using MedicaMap.DadosLocaisServices;
using MedicaMap.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace MedicaMap.Services;

public class BnafarImportService
{
    private readonly MedicaMapContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly ImportProgressStore _progressStore;

    public BnafarImportService(MedicaMapContext context, IWebHostEnvironment environment, ImportProgressStore progressStore)
    {
        _context = context;
        _environment = environment; 
        _progressStore = progressStore;
    }

    public async Task InstalarBancoAsync()
    {
        await ExecutarSqlAsync("BnafarRawCreate.sql");
        await ExecutarSqlAsync("TablesCreate.sql");
    }

    public async Task RemoverBancoAsync()
    {
        await ExecutarSqlAsync("DatabaseDrop.sql");
    }

    public async Task ImportarAsync(string caminhoArquivo, string importacaoId)
    {
        try
        {
            ValidarArquivo(caminhoArquivo);

            _progressStore.Atualizar(importacaoId, "Preparando banco e carregando CSV", 10);
            await PrepararImportacaoAsync(caminhoArquivo);
            _progressStore.Atualizar(importacaoId, "Importando municípios", 30);
            await ExecutarSqlAsync("MunicipalitiesImport.sql");
            _progressStore.Atualizar(importacaoId, "Importando estabelecimentos", 45);
            await ExecutarSqlAsync("EstablishmentsImport.sql");
            _progressStore.Atualizar(importacaoId, "Importando medicamentos", 60);
            await ExecutarSqlAsync("MedicationsImport.sql");
            _progressStore.Atualizar(importacaoId, "Importando estoques", 75);
            await ExecutarSqlAsync("StocksImport.sql");
            _progressStore.Concluir(importacaoId);
        }
        catch (Exception ex)
        {
            _progressStore.Falhar(importacaoId, ex.Message);
            throw;
        }
    }


    public async Task ImportarEstabelecimentosAsync(string caminhoArquivo, string importacaoId)
    {
        try
        {
            ValidarArquivo(caminhoArquivo);
            _progressStore.Atualizar(importacaoId, "Preparando banco e carregando CSV", 10);
            await PrepararImportacaoAsync(caminhoArquivo);
            _progressStore.Atualizar(importacaoId, "Importando municípios", 40);
            await ExecutarSqlAsync("MunicipalitiesImport.sql");
            _progressStore.Atualizar(importacaoId, "Importando estabelecimentos", 70);
            await ExecutarSqlAsync("EstablishmentsImport.sql");
            _progressStore.Concluir(importacaoId);
        }
        catch (Exception ex)
        {
            _progressStore.Falhar(importacaoId, ex.Message);
            throw;
        }
    }

    private void ValidarArquivo(string caminhoArquivo)
    {
        if (!File.Exists(caminhoArquivo))
        {
            throw new FileNotFoundException($"Arquivo BNAFAR não encontrado: {caminhoArquivo}");
        }
    }

    private async Task PrepararImportacaoAsync(string caminhoArquivo)
    {
        await ExecutarSqlAsync("BnafarRawCreate.sql");
        await ExecutarSqlAsync("TablesCreate.sql");
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE BnafarRaw;");
        await ImportarBnafarRawAsync(caminhoArquivo);
    }

    private async Task ExecutarSqlAsync(string nomeArquivo)
    {
        var caminho = Path.Combine(_environment.ContentRootPath, "Queries", nomeArquivo);

        if (!File.Exists(caminho))
        {
            throw new FileNotFoundException($"Arquivo SQL não encontrado: {caminho}");
        }

        var sql = await File.ReadAllTextAsync(caminho);
        await _context.Database.ExecuteSqlRawAsync(sql);
    }

    private async Task ImportarBnafarRawAsync(string caminhoArquivo)
    {
        var connection = (MySqlConnection)_context.Database.GetDbConnection();
        var abriuConexao = connection.State != System.Data.ConnectionState.Open;

        if (abriuConexao)
        {
            await connection.OpenAsync();
        }

        try
        {
            var loader = new MySqlBulkLoader(connection)
            {
                TableName = "BnafarRaw",
                FileName = caminhoArquivo,
                CharacterSet = "latin1",
                FieldTerminator = ";",
                FieldQuotationCharacter = '"',
                LineTerminator = "\r\n",
                NumberOfLinesToSkip = 1,
                Local = true
            };

            loader.Columns.AddRange(new[]
            {
                "sg_uf",
                "co_municipio_ibge",
                "no_municipio",
                "co_cnes",
                "no_razao_social",
                "no_fantasia",
                "co_cep",
                "no_logradouro",
                "nu_endereco",
                "no_bairro",
                "nu_telefone",
                "nu_latitude",
                "nu_longitude",
                "no_email",
                "dt_posicao_estoque",
                "co_catmat",
                "ds_produto",
                "qt_estoque",
                "nu_lote",
                "dt_validade",
                "tp_produto",
                "sg_programa_saude",
                "ds_programa_saude",
                "sg_origem"
            });

            await loader.LoadAsync();
        }
        finally
        {
            if (abriuConexao)
            {
                await connection.CloseAsync();
            }
        }
    }
}