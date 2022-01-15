using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class UserChangeUserRoomStatusCommand : IRequest<ResponseModel<int>>
    {
        public int RoomId { get; set; }
        public string UserId { get; set; }
        public string StatusId { get; set; }
        public string Reason { get; set; }
    }
}
