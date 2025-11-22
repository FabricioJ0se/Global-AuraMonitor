using AuraMonitor.Domain.Entities;
using AuraMonitor.Domain.Interfaces;
using AuraMonitor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuraMonitor.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuraMonitorContext _context;

    public UserRepository(AuraMonitorContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.MoodCheckins)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email.ToLower());
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .Where(u => u.IsActive)
            .ToListAsync();
    }

    public async Task<List<User>> GetByDepartmentAsync(Domain.Enums.Department department) // CORRIGIDO
    {
        return await _context.Users
            .Where(u => u.Department == department && u.IsActive)
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string email)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email.ToLower());
    }

    public async Task<int> CountAsync()
    {
        return await _context.Users
            .Where(u => u.IsActive)
            .CountAsync();
    }
}