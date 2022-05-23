using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;

namespace OnlineLearning.Queries
{
    public class GetInterestsQuery : IRequest<ResponseModel<PagedList<InterestDto>>>
    {
        public string UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
