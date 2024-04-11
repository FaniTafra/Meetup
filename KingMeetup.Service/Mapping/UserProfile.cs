
using AutoMapper;
using KingMeetup.Messaging;
using KingMeetup.Model;

namespace KingMeetup.Service.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRequest>()
                .ReverseMap();

            CreateMap<User, UserUpdateResponse>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<User, UserUpdateRequest>()
                .ReverseMap();

            CreateMap<UserUpdateResponse, UserUpdateRequest>()
                .ReverseMap();
        }
    }
}
