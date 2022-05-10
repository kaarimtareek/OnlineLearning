using System;

namespace OnlineLearning.Models.InputModels
{
    public class AddRoomMeetingInputModel
    {
        public bool StartNow { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public string ZoomToken { get; set; }
    }
}
