using AuraMonitor.Domain.Common;
using AuraMonitor.Domain.Enums;
using Ardalis.GuardClauses;

namespace AuraMonitor.Domain.Entities;

public class SensorReading : BaseEntity
{
    // Propriedades
    public SensorType SensorType { get; private set; }
    public decimal Value { get; private set; }
    public string Location { get; private set; }
    public DateTime ReadingDate { get; private set; }

    // Construtor privado para EF
    private SensorReading() { }

    public SensorReading(SensorType sensorType, decimal value, string location)
    {
        Guard.Against.NullOrWhiteSpace(location, nameof(location));

        // Regras de negócio
        CheckRule(new SensorValueMustBeValidRule(sensorType, value));
        CheckRule(new LocationMustBeValidRule(location));

        SensorType = sensorType;
        Value = value;
        Location = location.Trim();
        ReadingDate = DateTime.UtcNow;
    }

    // Método para verificar se é um valor de alerta
    public bool IsAlertLevel()
    {
        return SensorType switch
        {
            SensorType.Temperature => Value < 18 || Value > 26, // Temperatura ideal 18-26°C
            SensorType.Noise => Value > 65, // Ruído acima de 65dB é prejudicial
            SensorType.Luminosity => Value < 300 || Value > 500, // Luminosidade ideal 300-500 lux
            _ => false
        };
    }

    public string GetAlertDescription()
    {
        if (!IsAlertLevel()) return string.Empty;

        return SensorType switch
        {
            SensorType.Temperature when Value < 18 => "Temperatura muito baixa",
            SensorType.Temperature when Value > 26 => "Temperatura muito alta",
            SensorType.Noise when Value > 65 => "Nível de ruído excessivo",
            SensorType.Luminosity when Value < 300 => "Luminosidade insuficiente",
            SensorType.Luminosity when Value > 500 => "Luminosidade excessiva",
            _ => "Condição ambiental fora do ideal"
        };
    }
}

// Regras de negócio para SensorReading
public class SensorValueMustBeValidRule : IBusinessRule
{
    private readonly SensorType _sensorType;
    private readonly decimal _value;

    public SensorValueMustBeValidRule(SensorType sensorType, decimal value)
    {
        _sensorType = sensorType;
        _value = value;
    }

    public bool IsBroken()
    {
        return _sensorType switch
        {
            SensorType.Temperature => _value < -10 || _value > 50, // Range válido para temperatura
            SensorType.Noise => _value < 0 || _value > 130, // Range válido para ruído
            SensorType.Luminosity => _value < 0 || _value > 1000, // Range válido para luminosidade
            _ => true
        };
    }

    public string Message => "Valor do sensor fora do range permitido";
}

public class LocationMustBeValidRule : IBusinessRule
{
    private readonly string _location;

    public LocationMustBeValidRule(string location)
    {
        _location = location;
    }

    public bool IsBroken()
    {
        return string.IsNullOrWhiteSpace(_location) ||
               _location.Trim().Length < 2 ||
               _location.Trim().Length > 50;
    }

    public string Message => "Localização deve ter entre 2 e 50 caracteres";
}