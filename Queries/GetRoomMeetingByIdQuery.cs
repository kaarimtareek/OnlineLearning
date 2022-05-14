using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.Models.OutputModels;

namespace OnlineLearning.Queries
{
    public class GetRoomMeetingByIdQuery : IRequest<ResponseModel<RoomMeetingDto>>
    {
        public string UserId { get; set; }
        public int RoomMeetingId { get; set; }
    }
}
