using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Commands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ResponseModel<string>>

    {
        private readonly IIdentityService identityService;
        private readonly DbContextOptions<AppDbContext> contextOptions;

        public AddUserCommandHandler(IIdentityService identityService, DbContextOptions<AppDbContext> contextOptions)
        {
            this.identityService = identityService;
            this.contextOptions = contextOptions;
        }

        public async Task<ResponseModel<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //we need to pass down the dbcontext to the functions to be able to commit or undo the transactions
            using (var context = new AppDbContext(contextOptions))
            {
                //if we need to call more than one function , and one of them failed , use rollback to undo all the changes
                using var transactionScope = await context.Database.BeginTransactionAsync();
                {
                    try
                    {
                        var result = await identityService.Add(context, request.Name, request.Email, request.Phonenumber, request.Password, request.BrithDate);
                        if (!result.IsSuccess)
                        {
                            await transactionScope.RollbackAsync();
                        }
                        else
                        {
                            await transactionScope.CommitAsync();
                        }
                        return new ResponseModel<string>
                        {
                            IsSuccess = result.IsSuccess,
                            MessageCode = result.Message,
                            Result = result.Data,
                            HttpStatusCode = result.ResponseCode.GetStatusCode()
                        };
                    }
                    catch (Exception e)
                    {
                        await transactionScope.RollbackAsync();
                        return new ResponseModel<string>
                        {
                            IsSuccess = false,
                            HttpStatusCode = ResponseCodeEnum.FAILED.GetStatusCode()
                        };
                    }
                }
            }
        }
    }
}