using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class EditAnswerCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public int AnswerId { get; set; }
        public string AnswerDescription { get; set; }
    }
}
