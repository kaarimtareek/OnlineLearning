using System;
using System.Collections.Generic;

namespace OnlineLearning.DTOs.Reports
{
    public class RequestedRoomsActivityDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int TotalRequestedRooms { get => NumberOfJoinedRooms +NumberOfRequestedRooms + NumberOfRejectedRooms + NumberOfLeftRooms; }
        public int NumberOfRequestedRooms { get => RequestedRooms ==null? 0: RequestedRooms.Count; }
        public int NumberOfJoinedRooms { get => JoinedRooms ==null? 0: JoinedRooms.Count; }
        public int NumberOfRejectedRooms { get => RejectedRooms ==null? 0: RejectedRooms.Count; }
        public int NumberOfLeftRooms { get => LeftRooms ==null? 0: LeftRooms.Count; }
        public List<RequestedRoomDto> RequestedRooms { get; set; }
        public List<RequestedRoomDto> JoinedRooms { get; set; }
        public List<RequestedRoomDto> RejectedRooms { get; set; }
        public List<RequestedRoomDto> LeftRooms { get; set; }

    }
    public class RequestedRoomDto
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime FinalDate { get; set; }
        public string StatusId { get; set; }
        public string RoomName { get; set; }

    }
}
