using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Models
{
    public class RoomMeeting : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        [MaxLength(1000)]
        public string MeetingUrl { get; set; }
        [MaxLength(100)]
        public string StatusId { get; set; }
        [MaxLength(100)]
        public string OwnerId { get; set; }
        [MaxLength(200)]
        public string MeetingName { get; set; }
        [MaxLength(1000)]
        public string MeetingDescription { get; set; }
        [MaxLength(200)]
        public string MeetingPassword { get; set; }
        public int ZoomMeetingId { get; set; }
        public virtual Room Room { get; set; }
        public virtual LookupRoomMeetingStatus Status { get; set; }
    }
}
