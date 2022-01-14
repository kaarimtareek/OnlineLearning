using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace OnlineLearning.Models
{
    [Index(nameof(UserId),nameof(RoomId),IsUnique = true)]
    public class UsersRooms : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }
        public int RoomId { get; set; }
        [MaxLength(50)]
        public string StatusId { get; set; }
        [MaxLength(200)]
        public string Comment { get;set; }
        [MaxLength(200)]
        public string RejectionReason { get; set; }
        public virtual LookupUserRoomStatus Status { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Room Room { get; set; }
    }
}
