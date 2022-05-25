using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public decimal Price { get; set; }
        //indication if the room doesn't need approval or not
        public bool IsPublic { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int NumberOfJoinedUsers { get; set; }
        public int NumberOfRequestedUsers { get; set; }
        public int NumberOfRejectedUsers { get; set; }
        public int NumberOfLeftUsers { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<RoomInterest> RoomInterests { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual LookupRoomStatus Status { get; set; }
        public virtual ICollection<UsersRooms> RequestedUsers { get; set; }
        public virtual ICollection<RoomRoadMap> RoomRoadMaps { get; set; }
        public virtual ICollection<RoomMaterial> RoomMaterials { get; set; }
    }
}
