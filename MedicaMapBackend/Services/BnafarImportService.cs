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

    public async Task ImportarAsync(string caminhoArquivo)
    {
        await ExecutarSqlAsync("BnafarRawCreate.sql");
        await ExecutarSqlAsync("TablesCreate.sql");
        await ImportarBnafarRawAsync("BnafarRawImport.sql", caminhoArquivo);
        await ExecutarSqlAsync("MunicipalitiesCreate.sql");
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

    private async Task ImportarBnafarRawAsync(string nomeArquivoSql, string caminhoArquivo)
    {
        var caminhoSql = Path.Combine(_environment.ContentRootPath, "Queries", nomeArquivoSql);
        if (!File.Exists(caminhoSql))
        {
            throw new FileNotFoundException($"Arquivo SQL não encontrado: {caminhoSql}");
        }

        if (!File.Exists(caminhoArquivo))
        {
            throw new FileNotFoundException($"Arquivo BNAFAR não encontrado: {caminhoArquivo}");
        }

        var sql = await File.ReadAllTextAsync(caminhoSql);
        caminhoArquivo = caminhoArquivo.Replace("\\", "/");
        sql = sql.Replace("'C:/Planilha.csv'", $"'{caminhoArquivo}'");
        var connection = (MySqlConnection)_context.Database.GetDbConnection();

        if (connection.State != System.Data.ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        using var command = new MySqlCommand(sql, connection);
        await command.ExecuteNonQueryAsync();
    }
}