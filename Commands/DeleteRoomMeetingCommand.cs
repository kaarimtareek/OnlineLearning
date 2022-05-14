using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class DeleteRoomMeetingCommand : IRequest<ResponseModel<int>>
    {
        public int RoomId { get; set; }
        public string UserId { get; set; }
        public string ZoomToken { get; set; }
        public int MeetingId { get; set; }
    }
}
