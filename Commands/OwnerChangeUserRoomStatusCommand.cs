using MediatR;

using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class OwnerChangeUserRoomStatusCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public string OwnerId { get; set; }
        public int RoomId { get; set; }
        public string StatusId { get; set; }
        public string Reason { get; set; }
    }
}
