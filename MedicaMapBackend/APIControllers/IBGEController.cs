using MedicaMap.APIServices;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.APIControllers
{
    [ApiController]
    [Route("api/ibge")]
    public class IBGEController : ControllerBase
    { 
        private readonly IBGEAPIService _service;

        public IBGEController(IBGEAPIService service)
        {
            _service = service;
        }

        [HttpGet("estados")]
        public async Task<IActionResult> ObterEstados()
        {
            var estados = await _service.ObterEstadosAsync();
            return Ok(estados);
        }

        [HttpGet("estados/{codigoUf}/municipios")]
        public async Task<IActionResult> ObterMunicipios(int codigoUf)
        {
            var municipios = await _service.ObterMunicipiosAsync(codigoUf);
            return Ok(municipios);
        }
    }
}
