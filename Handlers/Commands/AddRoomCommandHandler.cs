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
    public class AddRoomCommandHandler : IRequestHandler<AddRoomCommand, ResponseModel<int>>

    {
        private readonly IRoomService roomService;
        private readonly DbContextOptions<AppDbContext> contextOptions;

        public AddRoomCommandHandler(DbContextOptions<AppDbContext> contextOptions, IRoomService roomService)
        {
            this.contextOptions = contextOptions;
            this.roomService = roomService;
        }

        public async Task<ResponseModel<int>> Handle(AddRoomCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                //if we need to call more than one function , and one of them failed , use rollback to undo all the changes
                using var transactionScope = await context.Database.BeginTransactionAsync();
                {
                    try
                    {
                        //validate the interests before creating the room
                      var result = await roomService.CreateRoom(context, request.UserId, request.RoomName, request.RoomDescription, request.Price, request.StartDate, request.ExpectedEndDate,request.IsPublic, request.Interests);
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
                        //TODO: log the error
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
}