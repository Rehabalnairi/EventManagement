using AutoMapper;
using EventManagement.DTO;
using EventManagement.Models; 
namespace EventManagement.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() // Add a constructor to define mappings
        {
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.AttendeeCount, opt => opt.MapFrom(src => src.Attendees.Count));
            CreateMap<CreateEventDto, Event>();

            // Attendee mappings
            CreateMap<Attendee, AttendeeDto>();
            CreateMap<CreateAttendeeDto, Attendee>();
        }
    }
}
