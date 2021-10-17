using MediatR;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Commands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand,ResponseModel<int>>

    {
        private readonly IUserService userService;

        public AddUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<ResponseModel<int>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
           var result = await userService.Add(request.Name, request.Email, request.Phonenumber, request.Password, request.BrithDate);

            return new ResponseModel<int>
            {
                IsSuccess = result.IsSuccess,
                MessageCode = result.Message,
                Result = result.Data,
                HttpStatusCode = result.ResponseCode.GetStatusCode()
            };
        }
    }
}
