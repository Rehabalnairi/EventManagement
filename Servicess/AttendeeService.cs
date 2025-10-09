using AutoMapper;
using EventManagement.DTO;
using EventManagement.Models;
using EventManagement.Repository;



namespace EventManagement.Services
{
    public class AttendeeService : IAttendeeService
    {
        //Depencencies
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

        public async Task<AttendeeDto> RegisterAttendeeAsync(CreateAttendeeDto dto)
        {
            var ev = await _eventRepository.GetByIdAsync(dto.EventId);
            if (ev == null)
                throw new Exception("Event not found.");

            if (ev.Attendees.Count >= ev.MaxAttendees)
                throw new Exception("Event is full.");

            var attendee = _mapper.Map<Attendee>(dto);
            var created = await _attendeeRepository.AddAsync(attendee);
            return _mapper.Map<AttendeeDto>(created);//send to API
        }

    }
}
