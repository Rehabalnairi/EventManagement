using EventManagement.Models;
using System.Linq.Expressions;

namespace EventManagement.Repository
{
    public interface IAttendeeRepository
    {
        Task<IEnumerable<Attendee>> GetByEventIdAsync(int eventId);
        Task<Attendee> AddAsync(Attendee attendee);
        Task<List<Attendee>> FindAsync(Expression<Func<Attendee, bool>> predicate);
       // Task<Attendee> UpdateAsync(Attendee attendee);
    }
}
