using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class EditQuestionCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionDescription{ get; set; }
    }
}
