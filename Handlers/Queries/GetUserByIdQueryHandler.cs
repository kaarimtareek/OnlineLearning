using MediatR;
using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using OnlineLearning.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseModel<ApplicationUser>>
    {
        private readonly IUserService userService;

        public GetUserByIdQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<ResponseModel<ApplicationUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
           var result = await userService.Get(request.Id);
            
                return new ResponseModel<ApplicationUser>
                {
                    IsSuccess = result.IsSuccess,
                    HttpStatusCode = result.ResponseCode.GetStatusCode(),
                    Result = result.Data,
                    MessageCode = result.Message,
                };
        }
    }
}
