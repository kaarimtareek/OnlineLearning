﻿using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Services;

namespace OnlineLearning.Handlers.Commands
{
    public class OwnerChangeUserRoomStatusCommandHandler : IRequestHandler<OwnerChangeUserRoomStatusCommand, ResponseModel<int>>
    {
        private readonly IRoomService roomService;
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public OwnerChangeUserRoomStatusCommandHandler(IRoomService roomService,DbContextOptions<AppDbContext> dbContextOptions)
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
                   var result = await roomService.ChangeUserRoomStatus(context, request.UserId, request.RoomId, request.StatusId, Constants.ConstantUserRoomStatus.RoomOwnerAllowedStatus, request.Reason);
                    if(!result.IsSuccess)
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
