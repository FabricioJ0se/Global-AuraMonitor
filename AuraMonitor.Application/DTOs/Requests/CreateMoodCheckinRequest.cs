namespace AuraMonitor.Application.DTOs.Requests;

public record CreateMoodCheckinRequest(
    int UserId,
    int MoodLevel,
    string? Notes,
    string? Factors
);
