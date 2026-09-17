using MedicaMap.Data;
using MedicaMap.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicaMap.Services;

public class StockService
{
    private readonly MedicaMapContext _context;

    public StockService(MedicaMapContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.AsNoTracking().Include(s => s.Establishment).Include(s => s.Medication).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(long id)
    {
        return await _context.Stocks.AsNoTracking().Include(s => s.Establishment).Include(s => s.Medication).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Stock>> GetByMunicipalityAsync(int municipalityId)
    {
        return await _context.Stocks.AsNoTracking().Include(s => s.Establishment).Include(s => s.Medication).Where(s => s.Establishment.MunicipalityId == municipalityId).ToListAsync();
    }

    public async Task<List<Stock>> GetByMedicationAsync(int medicationId)
    {
        return await _context.Stocks.AsNoTracking().Include(s => s.Establishment).Include(s => s.Medication).Where(s => s.MedicationId == medicationId).ToListAsync();
    }

    public async Task<List<Stock>> GetByEstablishmentAsync(int establishmentId)
    {
        return await _context.Stocks.AsNoTracking().Include(s => s.Establishment).Include(s => s.Medication).Where(s => s.EstablishmentId == establishmentId).OrderBy(s => s.Medication.Description).ToListAsync();
    }
}