using OnlineLearning.Constants;

using System;
using System.Collections.Generic;

namespace OnlineLearning.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusId { get; set; }
        public int NumberOfJoinedUsers { get; set; }
        public int NumberOfRequestedUsers { get; set; }
        public int NumberOfRejectedUsers { get; set; }
        public int NumberOfLeftUsers { get; set; }
        public RoomStatusDto Status { get; set; }
        private UserRoomStatusDto _status;
        public UserRoomStatusDto UserRoomStatus { get { return _status == null? new UserRoomStatusDto { Id = ConstantUserRoomStatus.NO_REQUEST , NameEnglish  = ConstantUserRoomStatus.NO_REQUEST_ENGLISH, NameArabic =ConstantUserRoomStatus.NO_REQUEST_ARABIC } :_status; } set { _status = value; } }
        public decimal Price { get; set; }
        public bool IsPublic { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public IEnumerable<InterestDto> Interests { get; set; }
    }
}
