using AuraMonitor.Domain.Common;
using AuraMonitor.Domain.Enums;
using Ardalis.GuardClauses;

namespace AuraMonitor.Domain.Entities;

public class MoodCheckin : BaseEntity
{
    // Propriedades
    public int UserId { get; private set; }
    public MoodLevel MoodLevel { get; private set; }
    public string? Notes { get; private set; }
    public string? Factors { get; private set; } // Fatores que influenciaram o humor
    public DateTime CheckinDate { get; private set; }

    // Navegação
    public virtual User User { get; private set; }

    // Construtor privado para EF
    private MoodCheckin() { }

    public MoodCheckin(int userId, MoodLevel moodLevel, string? notes, string? factors)
    {
        Guard.Against.NegativeOrZero(userId, nameof(userId));

        // Regras de negócio
        CheckRule(new MoodLevelMustBeValidRule(moodLevel));
        CheckRule(new NotesLengthMustBeValidRule(notes));

        UserId = userId;
        MoodLevel = moodLevel;
        Notes = notes?.Trim();
        Factors = factors?.Trim();
        CheckinDate = DateTime.UtcNow;
    }

    public void UpdateNotes(string notes)
    {
        CheckRule(new NotesLengthMustBeValidRule(notes));
        Notes = notes?.Trim();
        SetUpdatedAt();
    }
}

// Regras de negócio para MoodCheckin
public class MoodLevelMustBeValidRule : IBusinessRule
{
    private readonly MoodLevel _moodLevel;

    public MoodLevelMustBeValidRule(MoodLevel moodLevel)
    {
        _moodLevel = moodLevel;
    }

    public bool IsBroken()
    {
        return !Enum.IsDefined(typeof(MoodLevel), _moodLevel);
    }

    public string Message => "Nível de humor deve ser válido";
}

public class NotesLengthMustBeValidRule : IBusinessRule
{
    private readonly string? _notes;

    public NotesLengthMustBeValidRule(string? notes)
    {
        _notes = notes;
    }

    public bool IsBroken()
    {
        return _notes?.Length > 500;
    }

    public string Message => "Notas não podem exceder 500 caracteres";
}