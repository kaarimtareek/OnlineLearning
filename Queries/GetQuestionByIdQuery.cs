using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs;

using System.Collections.Generic;

namespace OnlineLearning.Queries
{
    public class GetQuestionByIdQuery : IRequest<ResponseModel<QuestionDto>>
    {
        public string UserId { get; set; }
        public int QuestionId { get; set; }
    }
}
