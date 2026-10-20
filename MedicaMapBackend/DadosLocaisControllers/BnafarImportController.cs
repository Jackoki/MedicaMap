using MedicaMap.DadosLocaisServices;
using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/bnafar")]
public class BnafarImportController : ControllerBase
{
    private readonly BnafarImportService _importService;
    private readonly ImportProgressStore _progressStore;

    public BnafarImportController(BnafarImportService importService, ImportProgressStore progressStore)
    {
        _importService = importService;
        _progressStore = progressStore;
    }

    [HttpGet("progresso/{id}")]
    public IActionResult ConsultarProgresso(string id)
    {
        var progresso = _progressStore.Consultar(id);

        if (progresso == null)
        {
            return NotFound(new
            {
                mensagem = "Importação não encontrada."
            });
        }

        return Ok(progresso);
    }

    [HttpPost("importar")]
    public async Task<IActionResult> Importar([FromForm] IFormFile arquivo, [FromForm] string importacaoId)
    {
        return await ProcessarArquivoAsync(arquivo, importacaoId, _importService.ImportarAsync, "Arquivo BNAFAR importado com sucesso.");
    }

    [HttpPost("importar-estabelecimentos")]
    public async Task<IActionResult> ImportarEstabelecimentos([FromForm] IFormFile arquivo, [FromForm] string importacaoId)
    {
        return await ProcessarArquivoAsync(arquivo, importacaoId, _importService.ImportarEstabelecimentosAsync, "Estabelecimentos importados com sucesso.");
    }

    private async Task<IActionResult> ProcessarArquivoAsync(IFormFile arquivo, string importacaoId, Func<string, string, Task> importar, string mensagemSucesso)
    {
        if (arquivo == null || arquivo.Length == 0)
        {
            return BadRequest(new
            {
                mensagem = "Nenhum arquivo foi enviado."
            });
        }

        if (string.IsNullOrWhiteSpace(importacaoId))
        {
            return BadRequest(new
            {
                mensagem = "O identificador da importação não foi informado."
            });
        }

        if (!Guid.TryParse(importacaoId, out _))
        {
            return BadRequest(new
            {
                mensagem = "O identificador da importação é inválido."
            });
        }

        var extensao = Path.GetExtension(arquivo.FileName);

        if (!string.Equals(extensao, ".csv", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(new
            {
                mensagem = "O arquivo deve estar no formato CSV."
            });
        }

        var pastaTemporaria = Path.Combine(Path.GetTempPath(), "MedicaMap");
        Directory.CreateDirectory(pastaTemporaria);

        var nomeArquivo = $"{Guid.NewGuid()}.csv";
        var caminhoArquivo = Path.Combine(pastaTemporaria, nomeArquivo);

        try
        {
            _progressStore.Atualizar(importacaoId, "Recebendo arquivo", 0);

            await using (var stream = new FileStream(caminhoArquivo, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await arquivo.CopyToAsync(stream);
            }

            _progressStore.Atualizar(importacaoId, "Iniciando processamento", 5);

            await importar(caminhoArquivo, importacaoId);

            return Ok(new
            {
                mensagem = mensagemSucesso,
                importacaoId
            });
        }
        catch
        {
            throw;
        }

        finally
        {
            if (System.IO.File.Exists(caminhoArquivo))
            {
                System.IO.File.Delete(caminhoArquivo);
            }
        }
    }

    [HttpPost("instalar-banco")]
    public async Task<IActionResult> Instalar()
    {
        await _importService.InstalarBancoAsync();

        return Ok(new
        {
            mensagem = "Banco de dados instalado com sucesso."
        });
    }

    [HttpDelete("remover-banco")]
    public async Task<IActionResult> Remover()
    {
        await _importService.RemoverBancoAsync();

        return Ok(new
        {
            mensagem = "Banco de dados removido com sucesso."
        });
    }
}