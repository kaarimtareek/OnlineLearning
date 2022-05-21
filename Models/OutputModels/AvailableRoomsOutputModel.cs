using OnlineLearning.DTOs;

using System.Collections.Generic;

namespace OnlineLearning.Models.OutputModels
{
    public class AvailableRoomsOutputModel
    {
        public Dictionary<string, IEnumerable<RoomDto>> Values { get; set; }
    }
}
