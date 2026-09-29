using MedicaMap.DTOs;
using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/bnafar")]
public class BnafarController : ControllerBase
{
    private readonly BnafarApiService _service;

    public BnafarController(BnafarApiService service)
    {
        _service = service;
    }

    [HttpGet("estoques")]
    public async Task<IActionResult> ObterEstoques([FromQuery] int codigoUf, [FromQuery] int codigoMunicipio, [FromQuery] int codigoCnes, [FromQuery] string? dataPosicaoEstoque = null)
    {
        var estoques = new List<BNAFAREstoqueDTO>();
        await foreach (var estoque in _service.ObterEstoquesAsync(codigoUf, codigoMunicipio, codigoCnes, dataPosicaoEstoque))
        {
            estoques.Add(estoque);
        }

        return Ok(estoques);
    }
}