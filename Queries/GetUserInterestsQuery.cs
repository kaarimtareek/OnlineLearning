using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;

using System.Collections.Generic;

namespace OnlineLearning.Queries
{
    public class GetUserInterestsQuery : IRequest<ResponseModel<List<InterestDto>>>
    {
        public string UserId { get; set; }
    }
}
