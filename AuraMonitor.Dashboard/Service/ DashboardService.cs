namespace AuraMonitor.Dashboard.Services;

public class DashboardService
{
	public Task<MoodStatisticsData> GetMoodStatisticsAsync(DateTime startDate, DateTime endDate)
	{
		var data = new MoodStatisticsData
		{
			PeriodStart = startDate,
			PeriodEnd = endDate,
			TotalCheckins = 42,
			AverageMood = 3.8,
			MoodDistribution = new Dictionary<string, int>
			{
				{ "VerySad", 2 },
				{ "Sad", 5 },
				{ "Neutral", 15 },
				{ "Happy", 12 },
				{ "VeryHappy", 8 }
			}
		};

		return Task.FromResult(data);
	}

	public Task<List<AlertData>> GetRecentAlertsAsync()
	{
		var alerts = new List<AlertData>
		{
			new AlertData
			{
				Id = 1,
				Type = "Sensor",
				Title = "Temperatura muito alta",
				Location = "Sala de Reuniões A",
				Value = 28.5m,
				Timestamp = DateTime.Now.AddHours(-2),
				Priority = "Medium"
			},
			new AlertData
			{
				Id = 2,
				Type = "Sensor",
				Title = "Nível de ruído excessivo",
				Location = "Área de Descanso",
				Value = 72.3m,
				Timestamp = DateTime.Now.AddHours(-1),
				Priority = "High"
			}
		};

		return Task.FromResult(alerts);
	}

	public Task<EnvironmentData> GetEnvironmentDataAsync()
	{
		var data = new EnvironmentData
		{
			AverageTemperature = 23.5m,
			AverageNoise = 45.2m,
			AverageLight = 380.0m,
			AlertCount = 2
		};

		return Task.FromResult(data);
	}
}

public class MoodStatisticsData
{
	public DateTime PeriodStart { get; set; }
	public DateTime PeriodEnd { get; set; }
	public int TotalCheckins { get; set; }
	public double AverageMood { get; set; }
	public Dictionary<string, int> MoodDistribution { get; set; } = new();
}

public class AlertData
{
	public int Id { get; set; }
	public string Type { get; set; } = string.Empty;
	public string Title { get; set; } = string.Empty;
	public string Location { get; set; } = string.Empty;
	public decimal Value { get; set; }
	public DateTime Timestamp { get; set; }
	public string Priority { get; set; } = string.Empty;
}

public class EnvironmentData
{
	public decimal AverageTemperature { get; set; }
	public decimal AverageNoise { get; set; }
	public decimal AverageLight { get; set; }
	public int AlertCount { get; set; }
}