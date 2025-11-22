using AuraMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuraMonitor.Infrastructure.Data;

public class AuraMonitorContext : DbContext
{
    public AuraMonitorContext(DbContextOptions<AuraMonitorContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<HRManager> HRManagers => Set<HRManager>();
    public DbSet<MoodCheckin> MoodCheckins => Set<MoodCheckin>();
    public DbSet<SensorReading> SensorReadings => Set<SensorReading>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações básicas - remover seed data por enquanto
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Name).HasMaxLength(100);
            entity.Property(u => u.Email).HasMaxLength(100);
        });

        modelBuilder.Entity<HRManager>(entity =>
        {
            entity.HasIndex(h => h.Email).IsUnique();
            entity.Property(h => h.Name).HasMaxLength(100);
            entity.Property(h => h.Email).HasMaxLength(100);
        });

        modelBuilder.Entity<MoodCheckin>(entity =>
        {
            entity.Property(m => m.Notes).HasMaxLength(500);
            entity.Property(m => m.Factors).HasMaxLength(200);
            entity.HasIndex(m => m.CheckinDate);
        });

        modelBuilder.Entity<SensorReading>(entity =>
        {
            entity.Property(s => s.Location).HasMaxLength(50);
            entity.HasIndex(s => s.ReadingDate);
        });
    }
}