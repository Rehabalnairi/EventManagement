using EventManagement.Models;
using EventManagement.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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


        public async Task<Attendee> AddAsync(Attendee attendee)
        {
            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();
            return attendee;
        }

        public async Task<List<Attendee>> FindAsync(Expression<Func<Attendee, bool>> predicate)
        {
            return await _context.Attendees.Where(predicate).ToListAsync();
        }
    }
}
