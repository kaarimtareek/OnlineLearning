using System.Collections.Generic;

namespace OnlineLearning.DTOs.Reports
{
    public class UserCreatedRoomsActivityDto
    {
        public string UserId { get; set; }
        public int NumberOfRooms { get => RoomActivities == null ? 0 : RoomActivities.Count; }
        public List<RoomActivityDto> RoomActivities { get; set; }
    }
    
}
