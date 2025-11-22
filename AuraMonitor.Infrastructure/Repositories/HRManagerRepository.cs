using AuraMonitor.Domain.Entities;
using AuraMonitor.Domain.Interfaces;
using AuraMonitor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuraMonitor.Infrastructure.Repositories;

public class HRManagerRepository : IHRManagerRepository
{
    private readonly AuraMonitorContext _context;

    public HRManagerRepository(AuraMonitorContext context)
    {
        _context = context;
    }

    public async Task<HRManager?> GetByIdAsync(int id)
    {
        return await _context.HRManagers
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<HRManager?> GetByEmailAsync(string email)
    {
        return await _context.HRManagers
            .FirstOrDefaultAsync(h => h.Email == email.ToLower());
    }

    public async Task<List<HRManager>> GetAllAsync()
    {
        return await _context.HRManagers
            .Where(h => h.IsActive)
            .ToListAsync();
    }

    public async Task AddAsync(HRManager manager)
    {
        await _context.HRManagers.AddAsync(manager);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(HRManager manager)
    {
        _context.HRManagers.Update(manager);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string email)
    {
        return await _context.HRManagers
            .AnyAsync(h => h.Email == email.ToLower());
    }
}