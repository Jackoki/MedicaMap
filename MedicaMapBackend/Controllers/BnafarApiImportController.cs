using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BnafarApiImportController : ControllerBase
{
    private readonly BnafarApiImportService _importService;

    public BnafarApiImportController(BnafarApiImportService importService)
    {
        _importService = importService;
    }

    [HttpPost]
    public async Task<IActionResult> Importar()
    {
        await _importService.ImportarAsync();

        return Ok(new
        {
            mensagem = "Dados da BNAFAR API importados com sucesso."
        });
    }

    [HttpPost("teste")]
    public async Task<IActionResult> TestarImportacao()
    {
        await _importService.TestarImportacaoAsync();

        return Ok(new
        {
            mensagem = "Teste de importação executado."
        });
    }
}