using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models
{
    public class UserRoomsHistory : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserRoomsId { get; set; }
        [MaxLength(450)]
        public string UserId { get; set; }
        public int RoomId { get; set; }
        [MaxLength(50)]
        public string StatusId { get; set; }
        [MaxLength(200)]
        public string Comment { get; set; }
        [MaxLength(200)]
        public string RejectionReason { get; set; }
        [MaxLength(200)]
        public string SuspensionReason { get; set; }
        [MaxLength(200)]
        public string LeaveReason { get; set; }
        public virtual UsersRooms UsersRooms { get; set; }
        public virtual ApplicationUser User{ get; set; }
    }
}
