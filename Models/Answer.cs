using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Models
{
    public class Answer : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public bool IsAccepted { get; set; }
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
    }
}
