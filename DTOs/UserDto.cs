using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace OnlineLearning.DTOs
{
    public class UserDto
    {
        public Guid Id { get;set; }
        public string Name { get; set; }
        public string StatusId { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public UserStatusDto Status { get; set; }
        public List<InterestDto> Interests { get; set; }
        public List<RoomDto> JoinedRooms { get; set; }
        public List<RoomDto> CreatedRooms { get; set; }
    }
}
