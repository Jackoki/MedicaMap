using MedicaMap.DTOs;
using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstablishmentController : ControllerBase
{
    private readonly EstablishmentService _establishmentService;

    public EstablishmentController(EstablishmentService establishmentService)
    {
        _establishmentService = establishmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<EstablishmentDTO>>> GetAll()
    {
        var establishments = await _establishmentService.GetAllAsync();

        var result = establishments.Select(e => new EstablishmentDTO
        {
            Id = e.Id,
            CnesCode = e.CnesCode,
            TradeName = e.TradeName,
            Cep = e.Cep,
            Street = e.Street,
            AddressNumber = e.AddressNumber,
            Neighborhood = e.Neighborhood,
            Phone = e.Phone,
            Email = e.Email,
            MunicipalityId = e.MunicipalityId,
            MunicipalityName = e.Municipality?.Name,
            MunicipalityState = e.Municipality?.State
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EstablishmentDTO>> GetById(int id)
    {
        var establishment = await _establishmentService.GetByIdAsync(id);

        if (establishment == null)
            return NotFound();

        var result = new EstablishmentDTO
        {
            Id = establishment.Id,
            CnesCode = establishment.CnesCode,
            TradeName = establishment.TradeName,
            Cep = establishment.Cep,
            Street = establishment.Street,
            AddressNumber = establishment.AddressNumber,
            Neighborhood = establishment.Neighborhood,
            Phone = establishment.Phone,
            Email = establishment.Email,
            MunicipalityId = establishment.MunicipalityId,
            MunicipalityName = establishment.Municipality?.Name,
            MunicipalityState = establishment.Municipality?.State
        };

        return Ok(result);
    }

    [HttpGet("municipality/{municipalityId:int}")]
    public async Task<ActionResult<List<EstablishmentDTO>>> GetByMunicipality(
        int municipalityId)
    {
        var establishments =
            await _establishmentService.GetByMunicipalityAsync(municipalityId);

        var result = establishments.Select(e => new EstablishmentDTO
        {
            Id = e.Id,
            CnesCode = e.CnesCode,
            TradeName = e.TradeName,
            Cep = e.Cep,
            Street = e.Street,
            AddressNumber = e.AddressNumber,
            Neighborhood = e.Neighborhood,
            Phone = e.Phone,
            Email = e.Email,
            MunicipalityId = e.MunicipalityId,
            MunicipalityName = e.Municipality?.Name,
            MunicipalityState = e.Municipality?.State
        }).ToList();

        return Ok(result);
    }
}