using MediatR;
using OnlineLearning.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Commands
{
    public class LoginUserCommand : IRequest<ResponseModel<string>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
