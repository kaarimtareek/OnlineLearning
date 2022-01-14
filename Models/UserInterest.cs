using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace OnlineLearning.Models
{
    [Index(nameof(UserId), nameof(InterestId),IsUnique = true)]
    public class UserInterest : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        [MaxLength(200)]
        public string InterestId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
