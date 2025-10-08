using EventManagement.Models;

namespace EventManagement.Services
{
    public interface IAttendeeService
    {
        Task<List<Attendee>> GetAttendeesByEventIdAsync(int eventId);
        Task<Attendee> RegisterAttendeeAsync(Attendee attendee);
    }
}