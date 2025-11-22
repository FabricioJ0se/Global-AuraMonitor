using AuraMonitor.Domain.Entities;
using AuraMonitor.Domain.Interfaces;
using AuraMonitor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuraMonitor.Infrastructure.Repositories;

public class MoodCheckinRepository : IMoodCheckinRepository
{
    private readonly AuraMonitorContext _context;

    public MoodCheckinRepository(AuraMonitorContext context)
    {
        _context = context;
    }

    public async Task<MoodCheckin?> GetByIdAsync(int id)
    {
        return await _context.MoodCheckins
            .Include(m => m.User)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<MoodCheckin>> GetByUserIdAsync(int userId)
    {
        return await _context.MoodCheckins
            .Where(m => m.UserId == userId)
            .OrderByDescending(m => m.CheckinDate)
            .ToListAsync();
    }

    public async Task<List<MoodCheckin>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.MoodCheckins
            .Include(m => m.User)
            .Where(m => m.CheckinDate >= startDate && m.CheckinDate <= endDate)
            .OrderByDescending(m => m.CheckinDate)
            .ToListAsync();
    }

    public async Task<List<MoodCheckin>> GetTodayCheckinsAsync()
    {
        var today = DateTime.UtcNow.Date;
        return await _context.MoodCheckins
            .Include(m => m.User)
            .Where(m => m.CheckinDate.Date == today)
            .ToListAsync();
    }

    public async Task<List<MoodCheckin>> GetUserCheckinsByPeriodAsync(int userId, DateTime startDate, DateTime endDate)
    {
        return await _context.MoodCheckins
            .Where(m => m.UserId == userId && m.CheckinDate >= startDate && m.CheckinDate <= endDate)
            .OrderBy(m => m.CheckinDate)
            .ToListAsync();
    }

    public async Task<bool> HasCheckinTodayAsync(int userId)
    {
        var today = DateTime.UtcNow.Date;
        return await _context.MoodCheckins
            .AnyAsync(m => m.UserId == userId && m.CheckinDate.Date == today);
    }

    public async Task AddAsync(MoodCheckin checkin)
    {
        await _context.MoodCheckins.AddAsync(checkin);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MoodCheckin checkin)
    {
        _context.MoodCheckins.Update(checkin);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.MoodCheckins.CountAsync();
    }

    public async Task<Dictionary<Domain.Enums.MoodLevel, int>> GetMoodStatisticsAsync(DateTime startDate, DateTime endDate) // CORRIGIDO
    {
        var statistics = await _context.MoodCheckins
            .Where(m => m.CheckinDate >= startDate && m.CheckinDate <= endDate)
            .GroupBy(m => m.MoodLevel)
            .Select(g => new { MoodLevel = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.MoodLevel, x => x.Count);

        return statistics;
    }

    public async Task<double> GetAverageMoodAsync(int userId, DateTime startDate, DateTime endDate)
    {
        var average = await _context.MoodCheckins
            .Where(m => m.UserId == userId && m.CheckinDate >= startDate && m.CheckinDate <= endDate)
            .AverageAsync(m => (double)m.MoodLevel);

        return Math.Round(average, 2);
    }
}