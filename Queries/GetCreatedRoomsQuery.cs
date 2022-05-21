using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.Models.OutputModels;

namespace OnlineLearning.Queries
{
    public class GetCreatedRoomsQuery : IRequest<ResponseModel<PagedList<RoomDto>>>
    {
        public string UserId { get; set; }
    }
}
