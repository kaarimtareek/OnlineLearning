using OnlineLearning.DTOs;

using System.Collections.Generic;

namespace OnlineLearning.Models.OutputModels
{
    public class SearchAllOutputModel
    {
        public int RoomsNumber => Rooms==null ? 0 : Rooms.Count;
        public int UsersNumber { get=> Users == null ? 0: Users.Count; }
        public int InterestsNumber { get=> Interests == null ? 0 : Interests.Count; }
        public List<RoomDto> Rooms { get; set; }
        public List<UserDto> Users { get; set; }
        public List<InterestDto> Interests { get; set; }
    }
}
