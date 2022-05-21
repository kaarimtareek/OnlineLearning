using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;

namespace OnlineLearning.Queries
{
    public class GetRoomMeetingByIdQuery : IRequest<ResponseModel<RoomMeetingDto>>
    {
        public string UserId { get; set; }
        public int RoomMeetingId { get; set; }
    }
}
