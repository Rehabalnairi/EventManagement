using AutoMapper;
using EventManagement.DTO;
using EventManagement.Models;
using EventManagement.Repository;
using EventManagement.Services;


using Microsoft.Extensions.Logging;

namespace EventManagement.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EventService> _logger;

        public EventService(IEventRepository eventRepository, IMapper mapper, ILogger<EventService> logger)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;

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
        public async Task<EventDto> CreateEventAsync(CreateEventDto dto)
        {
            if (dto.Date < DateTime.UtcNow)
                throw new Exception("Event date cannot be in the past.");

            var ev = _mapper.Map<Event>(dto);
            var created = await _eventRepository.AddAsync(ev);
            return _mapper.Map<EventDto>(created);
        }
        public async Task<List<EventDto>> GetEventsAsync(
            string? location = null,
            DateTime? date = null,
            bool sortByAttendeeCount = false)
        {
            var events = await _eventRepository.GetAllWithAttendeesAsync();

            // Filtering
            if (!string.IsNullOrEmpty(location))
                events = events.Where(e => e.Location.Contains(location, StringComparison.OrdinalIgnoreCase)).ToList();

            if (date.HasValue)
                events = events.Where(e => e.Date.Date == date.Value.Date).ToList();

            // Sorting
            if (sortByAttendeeCount)
                events = events.OrderByDescending(e => e.Attendees.Count).ToList();

            _logger.LogInformation("New event created successfully: {EventTitle}");
            return _mapper.Map<List<EventDto>>(events);
        }


    }
}
