using System.Collections.Generic;

using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.Models;

namespace OnlineLearning.Queries
{
    public class GetUserInterestsQuery : IRequest<ResponseModel<List<InterestDto>>>
    {
        public string UserId { get; set; }
    }
}
