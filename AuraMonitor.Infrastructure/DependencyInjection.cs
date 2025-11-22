using AuraMonitor.Domain.Interfaces;
using AuraMonitor.Infrastructure.Data;
using AuraMonitor.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuraMonitor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do SQLite
        services.AddDbContext<AuraMonitorContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        // Registrar repositórios
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMoodCheckinRepository, MoodCheckinRepository>();
        services.AddScoped<ISensorReadingRepository, SensorReadingRepository>();
        services.AddScoped<IHRManagerRepository, HRManagerRepository>();

        return services;
    }
}