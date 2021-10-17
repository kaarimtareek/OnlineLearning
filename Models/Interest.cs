using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Models
{
    public class Interest : BaseEntity
    {
        [Key]
        [MaxLength(200)]
        public string Id { get; set; }
        [MaxLength(200)]
        public string NameArabic { get; set; }
        [MaxLength(200)]
        public string NameEnglish { get; set; }
        public virtual ICollection<RoomInterest> RoomInterests { get; set; }
        public virtual ICollection<UserInterest> UserInterests { get; set; }
    }
}
