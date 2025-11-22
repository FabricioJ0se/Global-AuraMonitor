using AuraMonitor.Domain.Entities;
using AuraMonitor.Domain.Interfaces;
using AuraMonitor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuraMonitor.Infrastructure.Repositories;

public class SensorReadingRepository : ISensorReadingRepository
{
    private readonly AuraMonitorContext _context;

    public SensorReadingRepository(AuraMonitorContext context)
    {
        _context = context;
    }

    public async Task<SensorReading?> GetByIdAsync(int id)
    {
        return await _context.SensorReadings
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<SensorReading>> GetBySensorTypeAsync(Domain.Enums.SensorType sensorType)
    {
        return await _context.SensorReadings
            .Where(s => s.SensorType == sensorType)
            .OrderByDescending(s => s.ReadingDate)
            .ToListAsync();
    }

    public async Task<List<SensorReading>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.SensorReadings
            .Where(s => s.ReadingDate >= startDate && s.ReadingDate <= endDate)
            .OrderByDescending(s => s.ReadingDate)
            .ToListAsync();
    }

    public async Task<List<SensorReading>> GetByLocationAsync(string location)
    {
        return await _context.SensorReadings
            .Where(s => s.Location == location)
            .OrderByDescending(s => s.ReadingDate)
            .ToListAsync();
    }

    public async Task<List<SensorReading>> GetAlertReadingsAsync()
    {
        return await _context.SensorReadings
            .Where(s => s.IsAlertLevel())
            .OrderByDescending(s => s.ReadingDate)
            .ToListAsync();
    }

    public async Task<List<SensorReading>> GetLatestReadingsAsync(int count)
    {
        return await _context.SensorReadings
            .OrderByDescending(s => s.ReadingDate)
            .Take(count)
            .ToListAsync();
    }

    public async Task AddAsync(SensorReading reading)
    {
        await _context.SensorReadings.AddAsync(reading);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<SensorReading> readings)
    {
        await _context.SensorReadings.AddRangeAsync(readings);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.SensorReadings.CountAsync();
    }

    public async Task<decimal> GetAverageValueAsync(Domain.Enums.SensorType sensorType, DateTime startDate, DateTime endDate)
    {
        var average = await _context.SensorReadings
            .Where(s => s.SensorType == sensorType && s.ReadingDate >= startDate && s.ReadingDate <= endDate)
            .AverageAsync(s => s.Value);

        return Math.Round(average, 2);
    }

    public async Task<List<SensorReading>> GetReadingsWithAlertsAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.SensorReadings
            .Where(s => s.ReadingDate >= startDate && s.ReadingDate <= endDate && s.IsAlertLevel())
            .ToListAsync();
    }
}