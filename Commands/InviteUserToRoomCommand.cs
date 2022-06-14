using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class InviteUserToRoomCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public int RoomId { get; set; }
        public string OwnerId { get; set; }
    }
}
