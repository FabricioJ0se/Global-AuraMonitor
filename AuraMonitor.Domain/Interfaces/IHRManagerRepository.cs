using AuraMonitor.Domain.Entities;

namespace AuraMonitor.Domain.Interfaces;

public interface IHRManagerRepository
{
    Task<HRManager?> GetByIdAsync(int id);
    Task<HRManager?> GetByEmailAsync(string email);
    Task<List<HRManager>> GetAllAsync();
    Task AddAsync(HRManager manager);
    Task UpdateAsync(HRManager manager);
    Task<bool> ExistsAsync(string email);
}