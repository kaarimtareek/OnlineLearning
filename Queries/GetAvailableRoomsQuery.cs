
using MediatR;

using OnlineLearning.Common;
using OnlineLearning.Models.OutputModels;

namespace OnlineLearning.Queries
{
    public class GetAvailableRoomsQuery : IRequest<ResponseModel<AvailableRoomsOutputModel>>
    {
        public string UserId { get; set; }
    }
}
