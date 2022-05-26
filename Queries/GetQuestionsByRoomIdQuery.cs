using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs;

using System.Collections.Generic;

namespace OnlineLearning.Queries
{
    public class GetQuestionsByRoomIdQuery : IRequest<ResponseModel<List<QuestionDto>>>
    {
        public string UserId { get; set; }
        public int RoomId { get; set; }
    }
}
