using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/cnes")]
public class CnesController : ControllerBase
{
    private readonly EstablishmentAPIService _service;

    public CnesController(EstablishmentAPIService service)
    {
        _service = service;
    }

    [HttpGet("estabelecimentos")]
    public async Task<IActionResult> ObterEstabelecimentos([FromQuery] int codigoUf, [FromQuery] int codigoMunicipio)
    {
        var estabelecimentos = await _service.ObterEstabelecimentosPorMunicipioAsync(codigoUf, codigoMunicipio);

        return Ok(estabelecimentos);
    }
}
