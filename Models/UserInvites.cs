using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearning.Models
{
    public class UserInvites : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string UserId { get; set; }
        public int RoomId { get; set; }
        [MaxLength(50)]
        public string StatusId { get; set; }
        public virtual Room Room { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser Owner { get; set; }
    }
}
