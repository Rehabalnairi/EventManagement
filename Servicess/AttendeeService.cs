using AutoMapper;
using EventManagement.Models;
using EventManagement.Repository;



namespace EventManagement.Services
{
    public class AttendeeService : IAttendeeService
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public AttendeeService(IAttendeeRepository attendeeRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _attendeeRepository = attendeeRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<Attendee> RegisterAttendeeAsync(Attendee attendee)
        {
            var ev = await _eventRepository.GetByIdAsync(attendee.EventId);
            if (ev == null)
                throw new Exception("Event not found.");

            // Prevent registering more than MaxAttendees
            if (ev.Attendees.Count >= ev.MaxAttendees)
                throw new Exception("Event is full.");

            return await _attendeeRepository.AddAsync(attendee);
        }

        public async Task<List<Attendee>> GetAttendeesByEventIdAsync(int eventId)
        {
            return await _attendeeRepository.FindAsync(a => a.EventId == eventId);
        }
    }
}
