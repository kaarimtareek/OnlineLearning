using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.DTOs.Reports;

namespace OnlineLearning.Queries.Reports
{
    public class GetActivityForRoomQuery : IRequest<ResponseModel<RoomActivityDto>>
    {
        public string UserId { get; set; }
        public int RoomId { get; set; }
    }
}
