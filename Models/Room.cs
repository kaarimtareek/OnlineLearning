using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Models
{
    public class Room : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string OwnerId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string StatusId { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<RoomInterest> RoomInterests{ get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}
