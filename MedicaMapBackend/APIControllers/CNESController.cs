using MedicaMap.DTOs;
using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/cnes")]
public class CnesController : ControllerBase
{
    private readonly CnesApiService _service;

    public CnesController(CnesApiService service)
    {
        _service = service;
    }

    [HttpGet("estabelecimentos")]
    public async Task<IActionResult> ObterEstabelecimentos([FromQuery] int codigoUf, [FromQuery] int codigoMunicipio)
    {
        var estabelecimentos = new List<CNESEstabelecimentoDTO>();

        await foreach (var estabelecimento in _service.ObterEstabelecimentosAsync(codigoUf, codigoMunicipio))
        {
            estabelecimentos.Add(estabelecimento);
        }

        return Ok(estabelecimentos);
    }

    [HttpGet("estabelecimentos/{codigoCnes}")]
    public async Task<IActionResult> ObterEstabelecimento(int codigoCnes)
    {
        var estabelecimento = await _service.ObterEstabelecimentoAsync(codigoCnes);

        if (estabelecimento == null)
            return NotFound();

        return Ok(estabelecimento);
    }
}