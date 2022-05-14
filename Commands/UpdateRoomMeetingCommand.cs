using MediatR;
using OnlineLearning.Common;

using System;

namespace OnlineLearning.Commands
{
    public class UpdateRoomMeetingCommand : IRequest<ResponseModel<int>>
    {
        public int MeetingId { get; set; }
        public string MeetingName { get; set; }
        public bool StartNow { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string MeetingDescription { get; set; }
        public string UserId { get; set; }
        public int RoomId{ get; set; }
        public int? Duration{ get; set; }
        public string ZoomToken{ get; set; }
    }
}
