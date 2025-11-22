using AuraMonitor.Application.DTOs.Requests;
using AuraMonitor.Application.DTOs.Responses;
using AuraMonitor.Domain.Entities;
using AuraMonitor.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AuraMonitor.Application.UseCases.Users;

public interface ICreateUserUseCase
{
    Task<UserResponse> ExecuteAsync(CreateUserRequest request);
}

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateUserUseCase> _logger;

    public CreateUserUseCase(IUserRepository userRepository, ILogger<CreateUserUseCase> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<UserResponse> ExecuteAsync(CreateUserRequest request)
    {
        if (await _userRepository.ExistsAsync(request.Email))
        {
            throw new InvalidOperationException("Email já está em uso");
        }

        var department = (Domain.Enums.Department)request.Department;
        var passwordHash = $"hashed_{request.Password}";
        
        var user = new User(request.Name, request.Email, passwordHash, department);
        await _userRepository.AddAsync(user);

        _logger.LogInformation("Usuário criado com ID: {UserId}", user.Id);

        return new UserResponse(
            user.Id,
            user.Name,
            user.Email,
            (int)user.Department,
            user.Department.ToString(),
            user.IsActive,
            user.LastCheckinDate,
            user.CreatedAt
        );
    }
}
