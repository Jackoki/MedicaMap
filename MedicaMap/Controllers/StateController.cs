using MedicaMap.DTOs;
using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StateController : ControllerBase
{
    private readonly StateService _stateService;

    public StateController(StateService stateService)
    {
        _stateService = stateService;
    }

    [HttpGet]
    public async Task<ActionResult<List<StateDTO>>> GetAll()
    {
        var states = await _stateService.GetAllAsync();

        var result = states.Select(s => new StateDTO
        {
            Id = s.Id,
            IbgeCode = s.IbgeCode,
            Uf = s.Uf,
            Name = s.Name
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StateDTO>> GetById(int id)
    {
        var state = await _stateService.GetByIdAsync(id);

        if (state == null)
            return NotFound();

        var result = new StateDTO
        {
            Id = state.Id,
            IbgeCode = state.IbgeCode,
            Uf = state.Uf,
            Name = state.Name
        };

        return Ok(result);
    }
}