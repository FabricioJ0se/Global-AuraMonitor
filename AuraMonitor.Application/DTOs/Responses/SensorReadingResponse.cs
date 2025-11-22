namespace AuraMonitor.Application.DTOs.Responses;

public record SensorReadingResponse(
    int Id,
    int SensorType,
    string SensorTypeName,
    decimal Value,
    string Location,
    bool IsAlert,
    string? AlertDescription,
    DateTime ReadingDate
);
