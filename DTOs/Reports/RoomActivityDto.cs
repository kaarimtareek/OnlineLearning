using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace OnlineLearning.DTOs.Reports
{
    public class RoomActivityDto
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public string StatusId { get; set; }
        public decimal Price { get; set; }
        public bool IsPublic { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int NumberOfJoinedUsers { get; set; }
        public int NumberOfRequestedUsers { get; set; }
        public int NumberOfRejectedUsers { get; set; }
        public int NumberOfLeftUsers { get; set; }
        public int AddedMaterials { get => RoomMaterialsActivities == null ? 0 : RoomMaterialsActivities.Count; }
        public int RoomMeetings { get => RoomMeetingActivities == null ? 0 : RoomMeetingActivities.Count; }
        public List<UserRoomActivityDto> RejectedUserRoomActivities { get; set; }
        public List<UserRoomActivityDto> JoinedUserRoomActivities { get; set; }
        public List<UserRoomActivityDto> RequestedUserRoomActivities { get; set; }
        public List<UserRoomActivityDto> LeftUserRoomActivities { get; set; }
        public List<RoomMeetingActivityDto> RoomMeetingActivities { get; set; }
        public List<RoomMaterialActivityDto> RoomMaterialsActivities { get; set; }
    }
    public class UserRoomActivityDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string StatusId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserRoomHistoryDto> UserRoomHistories { get; set; }
    }
    public class UserRoomHistoryDto
    {
        public int UserRoomActivityId { get; set; }
        public string UserId { get; set; }
        public string StatusId { get; set; }
        public int RoomId { get; set; }
        public string Comment { get; set; }
        public string RejectionReason { get; set; }
        public string SuspensionReason { get; set; }
        public string LeaveReason { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class RoomMaterialActivityDto
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class RoomMeetingActivityDto
    {
        public int MeetingId { get; set; }
        public int RoomId { get; set; }
        public string TopicName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate{ get; set; }
    }
}
