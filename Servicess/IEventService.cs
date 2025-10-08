using EventManagement.Models;

namespace EventManagement.Services
{
    public interface IEventService
    {
        Task<Event> CreateEventAsync(Event newEvent);
        Task<List<Event>> GetAllEventsAsync();
        Task<List<Event>> GetEventsByLocationAsync(string location);
        Task<List<Event>> GetUpcomingEventsAsync(int days = 30);
    }
}