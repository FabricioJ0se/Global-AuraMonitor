using AuraMonitor.Dashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuraMonitor.Dashboard.Controllers;

public class HomeController : Controller
{
    private readonly DashboardService _dashboardService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, DashboardService dashboardService)
    {
        _logger = logger;
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var endDate = DateTime.UtcNow;
        var startDate = endDate.AddDays(-30);

        var moodData = await _dashboardService.GetMoodStatisticsAsync(startDate, endDate);
        var alerts = await _dashboardService.GetRecentAlertsAsync();
        var environmentData = await _dashboardService.GetEnvironmentDataAsync();

        var viewModel = new DashboardViewModel
        {
            MoodData = moodData,
            Alerts = alerts,
            EnvironmentData = environmentData
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}

public class DashboardViewModel
{
    public MoodStatisticsData MoodData { get; set; } = new();
    public List<AlertData> Alerts { get; set; } = new();
    public EnvironmentData EnvironmentData { get; set; } = new();
}