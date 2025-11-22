using AuraMonitor.Domain.Entities;

namespace AuraMonitor.Domain.Interfaces;

public interface IMoodCheckinRepository
{
    Task<MoodCheckin?> GetByIdAsync(int id);
    Task<List<MoodCheckin>> GetByUserIdAsync(int userId);
    Task<List<MoodCheckin>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<List<MoodCheckin>> GetTodayCheckinsAsync();
    Task<List<MoodCheckin>> GetUserCheckinsByPeriodAsync(int userId, DateTime startDate, DateTime endDate);
    Task<bool> HasCheckinTodayAsync(int userId);
    Task AddAsync(MoodCheckin checkin);
    Task UpdateAsync(MoodCheckin checkin);
    Task<int> CountAsync();

    // Métodos para estatísticas
    Task<Dictionary<Enums.MoodLevel, int>> GetMoodStatisticsAsync(DateTime startDate, DateTime endDate);
    Task<double> GetAverageMoodAsync(int userId, DateTime startDate, DateTime endDate);
}