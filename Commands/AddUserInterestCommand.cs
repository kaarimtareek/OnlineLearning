using MediatR;

using OnlineLearning.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Commands
{
    public class AddUserInterestCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public string InterestId { get; set; }
        public string Interest { get; set; }
        public bool IgnoreSimilarity { get; set; }
    }
}
