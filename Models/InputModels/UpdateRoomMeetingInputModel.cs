using System;

namespace OnlineLearning.Models.InputModels
{
    public class UpdateRoomMeetingInputModel
    {

        public string TopicName { get; set; }
        public bool StartNow { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string TopicDescription { get; set; }
        public int? Duration { get; set; }
        public string ZoomToken { get; set; }
    }
}
