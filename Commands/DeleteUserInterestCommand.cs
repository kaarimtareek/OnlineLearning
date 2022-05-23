using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class DeleteUserInterestCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public string InterestId { get; set; }
    }
}
