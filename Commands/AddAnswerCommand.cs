using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class AddAnswerCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public int RoomId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerDescription { get; set; }
    }
}
