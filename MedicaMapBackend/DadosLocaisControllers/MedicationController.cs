using MedicaMap.DTOs;
using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicationController : ControllerBase
{
    private readonly MedicationService _medicationService;

    public MedicationController(MedicationService medicationService)
    {
        _medicationService = medicationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MedicationDTO>>> GetAll()
    {
        var medications = await _medicationService.GetAllAsync();

        var result = medications.Select(m => new MedicationDTO
        {
            Id = m.Id,
            CatmatCode = m.CatmatCode,
            Description = m.Description,
            ProductType = m.ProductType
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MedicationDTO>> GetById(int id)
    {
        var medication = await _medicationService.GetByIdAsync(id);

        if (medication == null)
            return NotFound();

        var result = new MedicationDTO
        {
            Id = medication.Id,
            CatmatCode = medication.CatmatCode,
            Description = medication.Description,
            ProductType = medication.ProductType
        };

        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<MedicationDTO>>> Search(
        [FromQuery] string name)
    {
        var medications = await _medicationService.SearchAsync(name);

        var result = medications.Select(m => new MedicationDTO
        {
            Id = m.Id,
            CatmatCode = m.CatmatCode,
            Description = m.Description,
            ProductType = m.ProductType
        }).ToList();

        return Ok(result);
    }
}