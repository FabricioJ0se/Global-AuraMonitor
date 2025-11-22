namespace AuraMonitor.Application.DTOs.Requests;

public record CreateSensorReadingRequest(
    int SensorType,
    decimal Value,
    string Location
);

public record CreateBulkSensorReadingsRequest(
    List<CreateSensorReadingRequest> Readings
);
