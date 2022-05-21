using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;

namespace OnlineLearning.Queries
{
    public class GetRoomsByInterestIdQuery : IRequest<ResponseModel<PagedList<RoomDto>>>
    {
        public string UserId { get; set; }
        public string InterestId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
