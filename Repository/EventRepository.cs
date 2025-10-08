using EventManagement.DbContext;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore; // Add this using directive

namespace EventManagement.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly EventManagmentDbContext _context;

        public EventRepository(EventManagmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllWithAttendeesAsync()
        {
            return await _context.Events.Include(e => e.Attendees).ToListAsync();
        }

        public async Task<Event?> GetByIdWithAttendeesAsync(int id)
        {
            return await _context.Events.Include(e => e.Attendees)
                .FirstOrDefaultAsync(e => e.EventId == id);
        }
    }
}
