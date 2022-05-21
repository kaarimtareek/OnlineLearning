using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string StatusId { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Room> CreatedRooms { get; set; }
        public virtual ICollection<UsersRooms> RequestedRooms { get; set; }
        public virtual ICollection<UserInterest> UserInterests { get; set; }
        public virtual LookupUserStatus Status { get; set; }
    }
}
