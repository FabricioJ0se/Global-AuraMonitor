using AuraMonitor.Domain.Common;
using AuraMonitor.Domain.Enums;
using Ardalis.GuardClauses;

namespace AuraMonitor.Domain.Entities;

public class User : BaseEntity
{
    // Propriedades
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public Department Department { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? LastCheckinDate { get; private set; }

    // Navegação
    public virtual ICollection<MoodCheckin> MoodCheckins { get; private set; } = new List<MoodCheckin>();

    // Construtor privado para EF
    private User() { }

    public User(string name, string email, string passwordHash, Department department)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Guard.Against.NullOrWhiteSpace(email, nameof(email));
        Guard.Against.NullOrWhiteSpace(passwordHash, nameof(passwordHash));

        // Regras de negócio
        CheckRule(new EmailMustBeValidRule(email));
        CheckRule(new NameMustBeValidRule(name));

        Name = name.Trim();
        Email = email.Trim().ToLower();
        PasswordHash = passwordHash;
        Department = department;
        IsActive = true;
    }

    // Métodos de domínio
    public void UpdateLastCheckinDate()
    {
        LastCheckinDate = DateTime.UtcNow;
        SetUpdatedAt();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdatedAt();
    }

    public void UpdateProfile(string name, Department department)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        CheckRule(new NameMustBeValidRule(name));

        Name = name.Trim();
        Department = department;
        SetUpdatedAt();
    }
}

// Regras de negócio
public class EmailMustBeValidRule : IBusinessRule
{
    private readonly string _email;

    public EmailMustBeValidRule(string email)
    {
        _email = email;
    }

    public bool IsBroken()
    {
        return string.IsNullOrWhiteSpace(_email) ||
               !_email.Contains('@') ||
               !_email.Contains('.');
    }

    public string Message => "Email deve ser um endereço válido";
}

public class NameMustBeValidRule : IBusinessRule
{
    private readonly string _name;

    public NameMustBeValidRule(string name)
    {
        _name = name;
    }

    public bool IsBroken()
    {
        return string.IsNullOrWhiteSpace(_name) ||
               _name.Trim().Length < 2 ||
               _name.Trim().Length > 100;
    }

    public string Message => "Nome deve ter entre 2 e 100 caracteres";
}