using AutoMapper;
using EventManagement.Models;
using EventManagement.Repository;
using EventManagement.Services;



namespace EventManagement.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return (await _eventRepository.GetAllWithAttendeesAsync()).ToList();
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            // Validation: Date cannot be in the past
            if (newEvent.Date < DateTime.UtcNow)
                throw new Exception("Event date cannot be in the past.");

            return await _eventRepository.AddAsync(newEvent);
        }

        public async Task<List<Event>> GetEventsByLocationAsync(string location)
        {
            var allEvents = await _eventRepository.GetAllWithAttendeesAsync();
            return allEvents.Where(e => e.Location.Contains(location)).ToList();
        }


        public async Task<List<Event>> GetUpcomingEventsAsync(int days = 30)
        {
            var targetDate = DateTime.UtcNow.AddDays(days);
            var allEvents = await _eventRepository.GetAllWithAttendeesAsync();
            return allEvents.Where(e => e.Date <= targetDate && e.Date >= DateTime.UtcNow).ToList();
        }

    }
}
