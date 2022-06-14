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
    public class ChangeInviteStatusCommandHandler :  IRequestHandler<ChangeInviteStatusCommand, ResponseModel<int>>
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
    private readonly IRoomService roomService;

        public ChangeInviteStatusCommandHandler(IRoomService roomService, DbContextOptions<AppDbContext> contextOptions)
        {
            this.roomService=roomService;
            this.contextOptions=contextOptions;
        }

        public async Task<ResponseModel<int>> Handle(ChangeInviteStatusCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                using var transactionScope = await context.Database.BeginTransactionAsync();
                try
                {
                    var result = await roomService.ChangeInviteUser(context, request.InviteId,request.Status);
                    if (!result.IsSuccess)
                    {
                        await transactionScope.RollbackAsync();
                    }
                    else
                    {
                        await transactionScope.CommitAsync();
                    }
                    return new ResponseModel<int>
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
                    return new ResponseModel<int>
                    {
                        IsSuccess = false,
                        HttpStatusCode = ResponseCodeEnum.FAILED.GetStatusCode()
                    };
                }

            }
        }
    }
}
