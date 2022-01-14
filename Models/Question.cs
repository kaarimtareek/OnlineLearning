using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace OnlineLearning.Models
{
    [Index(nameof(UserId))]
    [Index(nameof(RoomId))]
    public class Question : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; }
        [MaxLength(50)]
        public string StatusId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(1500)]
        public string Body { get; set; }
        public virtual Room Room { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
