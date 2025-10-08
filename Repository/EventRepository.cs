using EventManagement.DbContext;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> AddAsync(Event newEvent)
        {
            var addedEvent = await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return addedEvent.Entity;
        }
    }
}
