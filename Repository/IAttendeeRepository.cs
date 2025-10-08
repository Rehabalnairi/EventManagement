using EventManagement.Models;

namespace EventManagement.Repository
{
    public interface IAttendeeRepository
    {
        Task<IEnumerable<Attendee>> GetByEventIdAsync(int eventId);
    }
}
