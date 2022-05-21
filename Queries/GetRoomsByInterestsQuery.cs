using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;

namespace OnlineLearning.Queries
{
    public class GetRoomsByInterestsQuery : IRequest<ResponseModel<PagedList<RoomDto>>>
    {
        public string[] Interests { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
