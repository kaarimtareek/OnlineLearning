using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models
{
    public class Interest : BaseEntity
    {
        [Key]
        [MaxLength(200)]
        public string Id { get; set; }
        [MaxLength(200)]
        public string StemmedName { get; set; }
        public virtual ICollection<RoomInterest> RoomInterests { get; set; }
        public virtual ICollection<UserInterest> UserInterests { get; set; }
    }
}
