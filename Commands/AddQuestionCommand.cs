using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class AddQuestionCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public int RoomId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
    }
}
