using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/bnafar")]
public class BnafarImportController : ControllerBase
{
    private readonly BnafarImportService _importService;

    public BnafarImportController(BnafarImportService importService)
    {
        _importService = importService;
    }

    [HttpPost("importar")]
    public async Task<IActionResult> Importar([FromForm] IFormFile arquivo)
    {
        if (arquivo == null || arquivo.Length == 0)
        {
            return BadRequest("Nenhum arquivo foi enviado.");
        }

        var extensao = Path.GetExtension(arquivo.FileName);

        if (!string.Equals(extensao, ".csv", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("O arquivo deve estar no formato CSV.");
        }

        var pastaTemporaria = Path.Combine(Path.GetTempPath(), "MedicaMap");
        Directory.CreateDirectory(pastaTemporaria);
        var nomeArquivo = $"{Guid.NewGuid()}.csv";
        var caminhoArquivo = Path.Combine(pastaTemporaria, nomeArquivo);

        try
        {
            await using (var stream = new FileStream(caminhoArquivo, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await arquivo.CopyToAsync(stream);
            }

            await _importService.ImportarAsync(caminhoArquivo);

            return Ok(new
            {
                mensagem = "Arquivo BNAFAR importado com sucesso."
            });
        }
        finally
        {
            if (System.IO.File.Exists(caminhoArquivo))
            {
                System.IO.File.Delete(caminhoArquivo);
            }
        }
    }
}