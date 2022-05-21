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
    public class JoinRoomCommandHandler : IRequestHandler<JoinRoomCommand, ResponseModel<int>>
    {
        private readonly IRoomService roomService;
        private readonly DbContextOptions<AppDbContext> contextOptions;

        public JoinRoomCommandHandler(DbContextOptions<AppDbContext> contextOptions, IRoomService roomService)
        {
            this.contextOptions = contextOptions;
            this.roomService = roomService;
        }

        public async Task<ResponseModel<int>> Handle(JoinRoomCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                using var transactionScope = await context.Database.BeginTransactionAsync();
                try
                {
                    var result = await roomService.RequestToJoinRoom(context, request.RoomId, request.UserId);
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
                        IsSuccess = true,
                        HttpStatusCode = result.ResponseCode.GetStatusCode(),
                        MessageCode = result.Message,
                        Result = result.Data,
                    };
                }
                catch (Exception e)
                {
                    await transactionScope.RollbackAsync();
                    return new ResponseModel<int>
                    {
                        IsSuccess = false,
                        HttpStatusCode = ResponseCodeEnum.FAILED.GetStatusCode(),
                        MessageCode = ConstantMessageCodes.OPERATION_FAILED,
                    };
                }
            }
        }
    }
}