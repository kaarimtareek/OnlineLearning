using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class AcceptAnswerCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public int AnswerId { get; set; }
    }
}
