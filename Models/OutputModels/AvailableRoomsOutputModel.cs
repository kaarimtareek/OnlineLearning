using System.Collections.Generic;

using OnlineLearning.DTOs;

namespace OnlineLearning.Models.OutputModels
{
    public class AvailableRoomsOutputModel
    {
        public Dictionary<string,IEnumerable<RoomDto>> Values{ get; set; }
    }
}
