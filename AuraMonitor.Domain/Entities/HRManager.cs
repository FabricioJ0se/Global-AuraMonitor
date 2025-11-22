using AuraMonitor.Domain.Common;
using Ardalis.GuardClauses;

namespace AuraMonitor.Domain.Entities;

public class HRManager : BaseEntity
{
    // Propriedades
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public bool IsActive { get; private set; }
    public string AccessLevel { get; private set; } // "Admin", "Viewer", etc.

    // Construtor privado para EF
    private HRManager() { }

    public HRManager(string name, string email, string passwordHash, string accessLevel = "Viewer")
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.NullOrWhiteSpace(email, nameof(email));
        Guard.Against.NullOrWhiteSpace(passwordHash, nameof(passwordHash));

        CheckRule(new EmailMustBeValidRule(email));
        CheckRule(new NameMustBeValidRule(name));
        CheckRule(new AccessLevelMustBeValidRule(accessLevel));

        Name = name.Trim();
        Email = email.Trim().ToLower();
        PasswordHash = passwordHash;
        AccessLevel = accessLevel;
        IsActive = true;
    }

    public void UpdateAccessLevel(string accessLevel)
    {
        CheckRule(new AccessLevelMustBeValidRule(accessLevel));
        AccessLevel = accessLevel;
        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdatedAt();
    }
}

// Regras de negócio para HRManager
public class AccessLevelMustBeValidRule : IBusinessRule
{
    private readonly string _accessLevel;

    public AccessLevelMustBeValidRule(string accessLevel)
    {
        _accessLevel = accessLevel;
    }

    public bool IsBroken()
    {
        var validLevels = new[] { "Admin", "Viewer", "Analyst" };
        return string.IsNullOrWhiteSpace(_accessLevel) ||
               !validLevels.Contains(_accessLevel);
    }

    public string Message => "Nível de acesso deve ser: Admin, Viewer ou Analyst";
}