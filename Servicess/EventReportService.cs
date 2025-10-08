using EventManagement.Models;
using EventManagement.Repository;
using EventManagement.Repository;
using System.Net.Http.Json;

namespace EventManagement.Services
{
    public class EventReportService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public EventReportService(IEventRepository eventRepository, IHttpClientFactory httpClientFactory)
        {
            _eventRepository = eventRepository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<EventReportDto>> GetUpcomingEventsWithWeatherAsync()
        {
            // Replace the call to FindAsync with a call to GetAllWithAttendeesAsync and filter the results in memory.
            var allEvents = await _eventRepository.GetAllWithAttendeesAsync();
            var upcomingEvents = allEvents.Where(e =>
                e.Date >= DateTime.UtcNow && e.Date <= DateTime.UtcNow.AddDays(30));


            var client = _httpClientFactory.CreateClient();

            var reports = new List<EventReportDto>();
            foreach (var ev in upcomingEvents)
            {
                // Example: Call Open-Meteo API
                var weather = await client.GetFromJsonAsync<WeatherResponse>(
                    $"https://api.open-meteo.com/v1/forecast?latitude=40.7128&longitude=-74.0060&hourly=temperature_2m");

                reports.Add(new EventReportDto
                {
                    EventId = ev.EventId,
                    Title = ev.Title,
                    Date = ev.Date,
                    Location = ev.Location,
                    AttendeeCount = ev.Attendees.Count,
                    WeatherForecast = weather?.Hourly.Temperature_2m?.FirstOrDefault() ?? 0
                });
            }

            return reports;
        }
    }

    public class EventReportDto
    {
        public int EventId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public int AttendeeCount { get; set; }
        public double WeatherForecast { get; set; }
    }

    // Example weather response class
    public class WeatherResponse
    {
        public HourlyHourly Hourly { get; set; } = null!;
    }

    public class HourlyHourly
    {
        public List<double> Temperature_2m { get; set; } = new List<double>();
    }
}
