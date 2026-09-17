using MedicaMap.Data;
using MedicaMap.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicaMap.Services;

public class StateService
{
    private readonly MedicaMapContext _context;

    public StateService(MedicaMapContext context)
    {
        _context = context;
    }

    public async Task<List<State>> GetAllAsync()
    {
        return await _context.States.AsNoTracking().OrderBy(s => s.Name).ToListAsync();
    }

    public async Task<State?> GetByIdAsync(int id)
    {
        return await _context.States.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    }
}