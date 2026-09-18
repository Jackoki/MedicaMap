using MedicaMap.DTOs;
using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MunicipalityController : ControllerBase
{
    private readonly MunicipalityService _municipalityService;

    public MunicipalityController(MunicipalityService municipalityService)
    {
        _municipalityService = municipalityService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MunicipalityDTO>>> GetAll()
    {
        var municipalities = await _municipalityService.GetAllAsync();

        var result = municipalities.Select(m => new MunicipalityDTO
        {
            Id = m.Id,
            IbgeCode = m.IbgeCode,
            Name = m.Name,
            StateId = m.StateId,
            StateUf = m.State.Uf,
            StateName = m.State.Name
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MunicipalityDTO>> GetById(int id)
    {
        var municipality = await _municipalityService.GetByIdAsync(id);

        if (municipality == null)
            return NotFound();

        var result = new MunicipalityDTO
        {
            Id = municipality.Id,
            IbgeCode = municipality.IbgeCode,
            Name = municipality.Name,
            StateId = municipality.StateId,
            StateUf = municipality.State.Uf,
            StateName = municipality.State.Name
        };

        return Ok(result);
    }

    [HttpGet("state/{stateId:int}")]
    public async Task<ActionResult<List<MunicipalityDTO>>> GetByState(int stateId)
    {
        var municipalities = await _municipalityService.GetByStateAsync(stateId);

        var result = municipalities.Select(m => new MunicipalityDTO
        {
            Id = m.Id,
            IbgeCode = m.IbgeCode,
            Name = m.Name,
            StateId = m.StateId
        }).ToList();

        return Ok(result);
    }
}