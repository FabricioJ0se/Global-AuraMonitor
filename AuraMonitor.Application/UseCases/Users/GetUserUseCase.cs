using AuraMonitor.Application.DTOs.Responses;
using AuraMonitor.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AuraMonitor.Application.UseCases.Users;

public interface IGetUserUseCase
{
    Task<UserResponse?> ExecuteAsync(int userId);
}

public class GetUserUseCase : IGetUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<GetUserUseCase> _logger;

    public GetUserUseCase(IUserRepository userRepository, ILogger<GetUserUseCase> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<UserResponse?> ExecuteAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        
        if (user == null)
        {
            _logger.LogWarning("Usuário com ID {UserId} não encontrado", userId);
            return null;
        }

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
