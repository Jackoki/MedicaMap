using MedicaMap.Data;
using MedicaMap.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicaMap.Services;

public class MedicationService
{
    private readonly MedicaMapContext _context;

    public MedicationService(MedicaMapContext context)
    {
        _context = context;
    }

    public async Task<List<Medication>> GetAllAsync()
    {
        return await _context.Medications.AsNoTracking().OrderBy(m => m.Description).ToListAsync();
    }

    public async Task<Medication?> GetByIdAsync(int id)
    {
        return await _context.Medications.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Medication>> SearchAsync(string name)
    {
        return await _context.Medications.AsNoTracking().Where(m => m.Description.Contains(name)).OrderBy(m => m.Description).ToListAsync();
    }
}