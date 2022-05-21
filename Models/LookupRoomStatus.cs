using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models
{
    public class LookupRoomStatus : BaseEntity
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string NameArabic { get; set; }
        [MaxLength(100)]
        public string NameEnglish { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }

    }
}
