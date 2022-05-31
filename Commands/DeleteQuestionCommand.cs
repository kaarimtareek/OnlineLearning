using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class DeleteQuestionCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
