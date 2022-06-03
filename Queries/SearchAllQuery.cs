using MediatR;
using OnlineLearning.Common;
using OnlineLearning.Models.OutputModels;

namespace OnlineLearning.Queries
{
    public class SearchAllQuery : IRequest<ResponseModel<SearchAllOutputModel>>
    {
        public string SearchValue { get; set; }
        public string UserId { get; set; }
    }
}
