using MediatR;

using OnlineLearning.Common;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Commands
{
    public class AddUserInterestCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        [Required]
        public List<string> Interests { get; set; }
    }
}
