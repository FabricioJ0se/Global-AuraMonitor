using AuraMonitor.Domain.Entities;

namespace AuraMonitor.Domain.Interfaces;

public interface ISensorReadingRepository
{
    Task<SensorReading?> GetByIdAsync(int id);
    Task<List<SensorReading>> GetBySensorTypeAsync(Enums.SensorType sensorType);
    Task<List<SensorReading>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<List<SensorReading>> GetByLocationAsync(string location);
    Task<List<SensorReading>> GetAlertReadingsAsync();
    Task<List<SensorReading>> GetLatestReadingsAsync(int count);
    Task AddAsync(SensorReading reading);
    Task AddRangeAsync(IEnumerable<SensorReading> readings);
    Task<int> CountAsync();

    // Métodos para análises
    Task<decimal> GetAverageValueAsync(Enums.SensorType sensorType, DateTime startDate, DateTime endDate);
    Task<List<SensorReading>> GetReadingsWithAlertsAsync(DateTime startDate, DateTime endDate);
}