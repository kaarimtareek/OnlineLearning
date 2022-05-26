﻿using MediatR;

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
    public class OwnerChangeUserRoomStatusCommandHandler : IRequestHandler<OwnerChangeUserRoomStatusCommand, ResponseModel<int>>
    {
        private readonly IRoomService roomService;
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public OwnerChangeUserRoomStatusCommandHandler(IRoomService roomService, DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.roomService = roomService;
            this.dbContextOptions = dbContextOptions;
        }
        public async Task<ResponseModel<int>> Handle(OwnerChangeUserRoomStatusCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                using var transactionScope = await context.Database.BeginTransactionAsync();
                try
                {
                    var roomResult = await context.UsersRooms.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.RoomId == request.RoomId);

                    var oldRoomStatus = roomResult==null ? "" : roomResult.StatusId;
                    var result = await roomService.ChangeUserRoomStatus(context, request.UserId, request.RoomId, request.StatusId, ConstantUserRoomStatus.RoomOwnerAllowedStatus, request.Reason);
                    if (!result.IsSuccess)
                    {
                        await transactionScope.RollbackAsync();
                        return ResponseModel.Fail<int>(result.Message, default, null, result.ResponseCode.GetStatusCode());
                    }
                    var changeUserNumberOldStatusResult = await roomService.UpdateNumberOfUsers(context, request.RoomId, oldRoomStatus, -1);
                    var changeUserNumbernewStatusResult = await roomService.UpdateNumberOfUsers(context, request.RoomId, request.StatusId, 1);
                    await transactionScope.CommitAsync();
                    return new ResponseModel<int>
                    {
                        IsSuccess = result.IsSuccess,
                        MessageCode = result.Message,
                        Result = result.Data,
                        HttpStatusCode = result.ResponseCode.GetStatusCode()
                    };
                }
                catch (Exception ex)
                {
                    await transactionScope.RollbackAsync();
                    return new ResponseModel<int>
                    {
                        IsSuccess = false,
                        MessageCode = ConstantMessageCodes.OPERATION_FAILED,
                        HttpStatusCode = ResponseCodeEnum.FAILED.GetStatusCode()
                    };

                }
            }
        }
    }
}
