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
using System.Transactions;

namespace OnlineLearning.Handlers.Commands
{
    public class AddUserInviteCommandHandler : IRequestHandler<InviteUserToRoomCommand, ResponseModel<int>>
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IRoomService roomService;

        public AddUserInviteCommandHandler(DbContextOptions<AppDbContext> contextOptions, IRoomService roomService)
        {
            this.contextOptions=contextOptions;
            this.roomService=roomService;
        }

        public async Task<ResponseModel<int>> Handle(InviteUserToRoomCommand request, CancellationToken cancellationToken)
        {
            using(var context = new AppDbContext(contextOptions))
            {
                using var transactionScope = await context.Database.BeginTransactionAsync();
                try
                {
                    var result = await roomService.InviteUserToRoom(context, request.RoomId, request.OwnerId, request.UserId);
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
