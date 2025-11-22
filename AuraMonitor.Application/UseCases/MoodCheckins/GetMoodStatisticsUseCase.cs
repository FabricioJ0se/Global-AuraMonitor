using AuraMonitor.Application.DTOs.Responses;
using AuraMonitor.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AuraMonitor.Application.UseCases.MoodCheckins;

public interface IGetMoodStatisticsUseCase
{
    Task<MoodStatisticsResponse> ExecuteAsync(DateTime startDate, DateTime endDate);
}

public class GetMoodStatisticsUseCase : IGetMoodStatisticsUseCase
{
    private readonly IMoodCheckinRepository _moodCheckinRepository;
    private readonly ILogger<GetMoodStatisticsUseCase> _logger;

    public GetMoodStatisticsUseCase(
        IMoodCheckinRepository moodCheckinRepository,
        ILogger<GetMoodStatisticsUseCase> logger)
    {
        _moodCheckinRepository = moodCheckinRepository;
        _logger = logger;
    }

    public async Task<MoodStatisticsResponse> ExecuteAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException("Data inicial não pode ser maior que data final");
        }

        var statistics = await _moodCheckinRepository.GetMoodStatisticsAsync(startDate, endDate);
        var totalCheckins = statistics.Sum(s => s.Value);

        double averageMood = 0;
        if (totalCheckins > 0)
        {
            var weightedSum = statistics.Sum(s => (int)s.Key * s.Value);
            averageMood = Math.Round((double)weightedSum / totalCheckins, 2);
        }

        var moodDistribution = statistics.ToDictionary(
            s => s.Key.ToString(),
            s => s.Value
        );

        _logger.LogInformation("Estatísticas geradas: {TotalCheckins} check-ins", totalCheckins);

        return new MoodStatisticsResponse(
            moodDistribution,
            averageMood,
            totalCheckins,
            startDate,
            endDate
        );
    }
}
