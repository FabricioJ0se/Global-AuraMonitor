namespace AuraMonitor.Application.DTOs.Requests;

public record CreateUserRequest(
    string Name,
    string Email,
    string Password,
    int Department
);
