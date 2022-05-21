using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models
{
    public class LookupUserRoomStatus : BaseEntity
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string NameArabic { get; set; }
        [MaxLength(100)]
        public string NameEnglish { get; set; }
        public virtual ICollection<UsersRooms> UsersRooms { get; set; }
    }
}
