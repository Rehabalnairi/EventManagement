using EventManagement.Models;
using EventManagement.DbContext;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Repository

{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly EventManagmentDbContext _context;
        public AttendeeRepository(EventManagmentDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Attendee>> GetByEventIdAsync(int eventId)
        {
            return await _context.Attendees
            .Where(a => a.EventId == eventId)
            .ToListAsync();
        }


    }
}
