namespace EventManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Services;
using EventManagement.Models;
using System.Net.Http.Json;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    //Dependency Injection
    private readonly EventReportService _reportService;
    private readonly IHttpClientFactory _httpClientFactory;

    public ReportController(EventReportService reportService, IHttpClientFactory httpClientFactory)
    {
        _reportService = reportService;
        _httpClientFactory = httpClientFactory;
    }

    // GET /api/report/upcoming
    // Update the ReportController to use the correct method name
    // show Events with watherStute
    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingEventsWithWeather()
    {
        var events = await _reportService.GetUpcomingEventsWithWeatherAsync(); // Correct method name

        // External Weather API integration
        var client = _httpClientFactory.CreateClient();//for get outsource result
        foreach (var ev in events)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude=40.7128&longitude=-74.0060&hourly=temperature_2m";
            var response = await client.GetStringAsync(url);
            using var doc = System.Text.Json.JsonDocument.Parse(response);

 // get whater stute 
            double temp = doc.RootElement
                             .GetProperty("hourly")
                             .GetProperty("temperature_2m")[0]
                             .GetDouble();
              ev.WeatherForecast = temp;
        }

        return Ok(events);
    }

}

