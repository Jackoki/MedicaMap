using MedicaMap.Data;
using MedicaMap.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicaMap.Services;

public class EstablishmentService
{
    private readonly MedicaMapContext _context;

    public EstablishmentService(MedicaMapContext context)
    {
        _context = context;
    }

    public async Task<List<Establishment>> GetAllAsync()
    {
        return await _context.Establishments.AsNoTracking().Include(e => e.Municipality).OrderBy(e => e.TradeName).ToListAsync();
    }

    public async Task<Establishment?> GetByIdAsync(int id)
    {
        return await _context.Establishments.AsNoTracking().Include(e => e.Municipality).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Establishment>> GetByMunicipalityAsync(int municipalityId)
    {
        return await _context.Establishments.AsNoTracking().Where(e => e.MunicipalityId == municipalityId).OrderBy(e => e.TradeName).ToListAsync();
    }
}