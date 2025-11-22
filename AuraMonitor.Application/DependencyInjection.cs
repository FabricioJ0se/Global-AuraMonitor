using AuraMonitor.Application.UseCases.MoodCheckins;
using AuraMonitor.Application.UseCases.Users;
using Microsoft.Extensions.DependencyInjection;

namespace AuraMonitor.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Use Cases - Users
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<IGetUserUseCase, GetUserUseCase>();

        // Use Cases - Mood Checkins
        services.AddScoped<ICreateMoodCheckinUseCase, CreateMoodCheckinUseCase>();
        services.AddScoped<IGetMoodStatisticsUseCase, GetMoodStatisticsUseCase>();

        return services;
    }
}