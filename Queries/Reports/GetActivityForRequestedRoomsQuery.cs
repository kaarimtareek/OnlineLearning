using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs.Reports;

namespace OnlineLearning.Queries.Reports
{
    public class GetActivityForRequestedRoomsQuery : IRequest<ResponseModel<RequestedRoomsActivityDto>>
    {
        public string UserId { get; set; }
    }
}
