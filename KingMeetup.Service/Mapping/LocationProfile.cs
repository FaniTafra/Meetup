using AutoMapper;
using KingMeetup.Messaging;
using KingMeetup.Model;

namespace KingMeetup.Service.Mapping
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<State, StateResponse>()
                .ReverseMap();
            CreateMap<City, CityRequest>()
                .ReverseMap();
        }
    }
}
