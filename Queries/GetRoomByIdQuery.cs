using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;

namespace OnlineLearning.Queries
{
    public class GetRoomByIdQuery : IRequest<ResponseModel<RoomDto>>
    {
        public int RoomId { get; set; }
    }
}
