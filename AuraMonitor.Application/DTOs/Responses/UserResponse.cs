namespace AuraMonitor.Application.DTOs.Responses;

public record UserResponse(
    int Id,
    string Name,
    string Email,
    int Department,
    string DepartmentName,
    bool IsActive,
    DateTime? LastCheckinDate,
    DateTime CreatedAt
);
