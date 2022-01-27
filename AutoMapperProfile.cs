using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using OnlineLearning.DTOs;
using OnlineLearning.Models;

namespace OnlineLearning
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Room, RoomDto>()
                .ForMember(x => x.Interests, opt => opt.MapFrom(x => x.RoomInterests))
                .ForMember(x => x.OwnerName, opt => opt.MapFrom(x => x.Owner == null ? string.Empty: x.Owner.Name));
            CreateMap<RoomInterest, InterestDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.InterestId));
            CreateMap<LookupRoomStatus, RoomStatusDto>();
        }
    }
}
