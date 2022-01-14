using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace OnlineLearning.Models
{
    [Index(nameof(RoomId),nameof(InterestId),IsUnique = true)]
    public class RoomInterest : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomId { get; set; }
        [MaxLength(200)]
        public string InterestId { get; set; }
        public virtual Room Room { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
