using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearning.Models
{
    public class Answer : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string UserId { get; set; }
        [MaxLength(1000)]
        public string Body { get; set; }
        public bool IsAccepted { get; set; }
        public virtual Question Question { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
