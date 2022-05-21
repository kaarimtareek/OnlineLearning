using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.InputModels
{
    public class CreateMeetingInputModel
    {
        [Required]
        [MinLength(5)]
        public string TopicName { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int RoomId { get; set; }
        public bool CreateNow { get; set; }
    }
}
