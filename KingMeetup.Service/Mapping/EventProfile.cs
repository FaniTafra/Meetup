using AutoMapper;
using KingMeetup.Messaging;
using KingMeetup.Model;

namespace KingMeetup.Service.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventRequest>()
                .ReverseMap();
            CreateMap<AttendeeList, AttendeeListsRequest>() 
                .ReverseMap();
            CreateMap<Event, EventResponse>()
                .ForMember(dest => dest.Request, opt => opt.MapFrom(src => src))
                .ReverseMap();
        }
    }
}
