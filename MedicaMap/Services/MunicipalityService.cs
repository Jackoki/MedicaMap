using MedicaMap.Data;
using MedicaMap.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicaMap.Services;

public class MunicipalityService
{
    private readonly MedicaMapContext _context;

    public MunicipalityService(MedicaMapContext context)
    {
        _context = context;
    }

    public async Task<List<Municipality>> GetAllAsync()
    {
        return await _context.Municipalities.AsNoTracking().Include(m => m.State).OrderBy(m => m.Name).ToListAsync();
    }

    public async Task<Municipality?> GetByIdAsync(int id)
    {
        return await _context.Municipalities.AsNoTracking().Include(m => m.State).FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Municipality>> GetByStateAsync(int stateId)
    {
        return await _context.Municipalities.AsNoTracking().Where(m => m.StateId == stateId).OrderBy(m => m.Name).ToListAsync();
    }
}