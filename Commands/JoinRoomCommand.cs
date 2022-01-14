using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class JoinRoomCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public int RoomId { get; set; }
    }
}
