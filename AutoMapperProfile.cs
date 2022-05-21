
using AutoMapper;

using OnlineLearning.DTOs;
using OnlineLearning.Models;

using System.Linq;

namespace OnlineLearning
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Room, RoomDto>()
                .ForMember(x => x.Interests, opt => opt.MapFrom(x => x.RoomInterests))
                .ForMember(x => x.UserRoomStatus, opt => opt.MapFrom(x =>x.RequestedUsers == null? null : x.RequestedUsers.FirstOrDefault()))
                .ForMember(x => x.OwnerName, opt => opt.MapFrom(x => x.Owner == null ? string.Empty : x.Owner.Name));
            CreateMap<RoomInterest, InterestDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.InterestId));
            CreateMap<LookupRoomStatus, RoomStatusDto>();
            CreateMap<LookupUserRoomStatus, UserRoomStatusDto>();
        }
    }
}
