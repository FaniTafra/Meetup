using AutoMapper;
using KingMeetup.Messaging;
using KingMeetup.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingMeetup.Service.Mapping
{
    public class InterestProfile : Profile
    {
        public InterestProfile() 
        {
            CreateMap<Interest, InterestResponse>()
                .ReverseMap();
            CreateMap<UsersInterest, UsersInterestRequest>()
                .ReverseMap();
        }
    }
}
