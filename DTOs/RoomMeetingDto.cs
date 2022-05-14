using System.ComponentModel.DataAnnotations;
using System;

namespace OnlineLearning.DTOs
{
    public class RoomMeetingDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string MeetingUrl { get; set; }
        public string StatusId { get; set; }
        public string OwnerId { get; set; }
        public string MeetingName { get; set; }
        public string MeetingDescription { get; set; }
        public string MeetingPassword { get; set; }
        public long ZoomMeetingId { get; set; }
        public RoomMeetingStatusDto Status { get; set; } 

    }
}
