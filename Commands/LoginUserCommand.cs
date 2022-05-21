using MediatR;

using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class LoginUserCommand : IRequest<ResponseModel<string>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
