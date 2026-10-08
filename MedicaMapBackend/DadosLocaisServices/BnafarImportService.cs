using MedicaMap.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace MedicaMap.Services;

public class BnafarImportService
{
    private readonly MedicaMapContext _context;
    private readonly IWebHostEnvironment _environment;

    public BnafarImportService(MedicaMapContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
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

    public async Task ImportarAsync(string caminhoArquivo)
    {
        await ExecutarSqlAsync("BnafarRawCreate.sql");
        await ExecutarSqlAsync("TablesCreate.sql");
        await ImportarBnafarRawAsync(caminhoArquivo);
        await ExecutarSqlAsync("MunicipalitiesImport.sql");
        await ExecutarSqlAsync("EstablishmentsImport.sql");
        await ExecutarSqlAsync("MedicationsImport.sql");
        await ExecutarSqlAsync("StocksImport.sql");
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
        if (!File.Exists(caminhoArquivo))
        {
            throw new FileNotFoundException($"Arquivo BNAFAR não encontrado: {caminhoArquivo}");
        }

        var connection = (MySqlConnection)_context.Database.GetDbConnection();

        if (connection.State != System.Data.ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

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
}