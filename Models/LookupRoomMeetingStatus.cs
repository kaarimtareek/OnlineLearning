using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models
{
    public class LookupRoomMeetingStatus : BaseEntity
    {
        [Key]
        [MaxLength(100)]
        public string Id { get; set; }
        [MaxLength(200)]
        public string NameArabic { get; set; }
        [MaxLength(200)]
        public string NameEnglish { get; set; }
    }
}
