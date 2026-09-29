using MedicaMap.DTOs;
using MedicaMap.Models;
using MedicaMap.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedicaMap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly StockService _stockService;

    public StockController(StockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<ActionResult<List<StockDTO>>> GetAll()
    {
        var stocks = await _stockService.GetAllAsync();

        var result = stocks.Select(s => new StockDTO
        {
            Id = s.Id,
            EstablishmentId = s.EstablishmentId,
            EstablishmentName = s.Establishment.TradeName,
            MedicationId = s.MedicationId,
            MedicationDescription = s.Medication.Description,
            CatmatCode = s.Medication.CatmatCode,
            StockDate = s.StockDate,
            Quantity = s.Quantity
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<StockDTO>> GetById(long id)
    {
        var stock = await _stockService.GetByIdAsync(id);

        if (stock == null)
            return NotFound();

        var result = new StockDTO
        {
            Id = stock.Id,
            EstablishmentId = stock.EstablishmentId,
            EstablishmentName = stock.Establishment.TradeName,
            MedicationId = stock.MedicationId,
            MedicationDescription = stock.Medication.Description,
            CatmatCode = stock.Medication.CatmatCode,
            StockDate = stock.StockDate,
            Quantity = stock.Quantity
        };

        return Ok(result);
    }

    [HttpGet("establishment/{establishmentId:int}")]
    public async Task<ActionResult<List<StockDTO>>> GetByEstablishment(int establishmentId)
    {
        var stocks = await _stockService.GetByEstablishmentAsync(establishmentId);

        var result = stocks.Select(s => new StockDTO
        {
            Id = s.Id,
            EstablishmentId = s.EstablishmentId,
            EstablishmentName = s.Establishment.TradeName,
            MedicationId = s.MedicationId,
            MedicationDescription = s.Medication.Description,
            CatmatCode = s.Medication.CatmatCode,
            StockDate = s.StockDate,
            Quantity = s.Quantity
        }).ToList();

        return Ok(result);
    }
}