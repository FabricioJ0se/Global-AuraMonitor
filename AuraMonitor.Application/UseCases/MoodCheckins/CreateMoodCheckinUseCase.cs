using AuraMonitor.Application.DTOs.Requests;
using AuraMonitor.Application.DTOs.Responses;
using AuraMonitor.Domain.Entities;
using AuraMonitor.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AuraMonitor.Application.UseCases.MoodCheckins;

public interface ICreateMoodCheckinUseCase
{
    Task<MoodCheckinResponse> ExecuteAsync(CreateMoodCheckinRequest request);
}

public class CreateMoodCheckinUseCase : ICreateMoodCheckinUseCase
{
    private readonly IMoodCheckinRepository _moodCheckinRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateMoodCheckinUseCase> _logger;

    public CreateMoodCheckinUseCase(
        IMoodCheckinRepository moodCheckinRepository,
        IUserRepository userRepository,
        ILogger<CreateMoodCheckinUseCase> logger)
    {
        _moodCheckinRepository = moodCheckinRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<MoodCheckinResponse> ExecuteAsync(CreateMoodCheckinRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new InvalidOperationException("Usuário não encontrado");
        }

        var moodLevel = (Domain.Enums.MoodLevel)request.MoodLevel;
        var checkin = new MoodCheckin(request.UserId, moodLevel, request.Notes, request.Factors);
        
        await _moodCheckinRepository.AddAsync(checkin);

        user.UpdateLastCheckinDate();
        await _userRepository.UpdateAsync(user);

        _logger.LogInformation("Check-in criado: {CheckinId}", checkin.Id);

        return new MoodCheckinResponse(
            checkin.Id,
            checkin.UserId,
            user.Name,
            (int)checkin.MoodLevel,
            checkin.MoodLevel.ToString(),
            checkin.Notes,
            checkin.Factors,
            checkin.CheckinDate
        );
    }
}
