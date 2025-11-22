namespace AuraMonitor.Application.DTOs.Responses;

public record MoodCheckinResponse(
    int Id,
    int UserId,
    string UserName,
    int MoodLevel,
    string MoodLevelName,
    string? Notes,
    string? Factors,
    DateTime CheckinDate
);

public record MoodStatisticsResponse(
    Dictionary<string, int> MoodDistribution,
    double AverageMood,
    int TotalCheckins,
    DateTime PeriodStart,
    DateTime PeriodEnd
);
