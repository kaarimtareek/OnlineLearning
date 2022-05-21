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
    public class CreateRoomMeetingCommandHandler : IRequestHandler<AddRoomMeetingCommand, ResponseModel<int>>

    {
        private readonly IZoomService zoomService;
        private readonly IMeetingService meetingService;
        private readonly DbContextOptions<AppDbContext> contextOptions;

        public CreateRoomMeetingCommandHandler(IZoomService zoomService, IMeetingService meetingService, DbContextOptions<AppDbContext> contextOptions)
        {
            this.meetingService = meetingService;
            this.zoomService = zoomService;
            this.contextOptions = contextOptions;
        }

        public async Task<ResponseModel<int>> Handle(AddRoomMeetingCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                //if we need to call more than one function , and one of them failed , use rollback to undo all the changes
                using var transactionScope = await context.Database.BeginTransactionAsync();
                {
                    try
                    {
                        OperationResult<int> result;
                        var zoomResult = await zoomService.CreateZoomMeeting(request.ZoomToken, new Models.NetworkModels.UpsertZoomMeetingRequest
                        {
                            agenda = request.TopicName,
                            default_password =true,
                            duration = request.Duration,
                            password = "123123123",
                            pre_schedule = false,
                            start_time = request.StartTime,
                            timezone = "Africa/Cairo",
                            type = 2,
                            topic = request.TopicName
                        });

                        if (!zoomResult.IsSuccess)
                        {
                            await transactionScope.RollbackAsync();
                            return new ResponseModel<int>
                            {
                                IsSuccess = zoomResult.IsSuccess,
                                MessageCode = zoomResult.Message,
                                HttpStatusCode = zoomResult.ResponseCode.GetStatusCode()
                            };
                        }
                        result = await meetingService.AddMeeting(context, request.UserId, request.RoomId, zoomResult.Data.join_url, request.StartTime, request.EndTime, request.TopicName, request.TopicDescription, zoomResult.Data.id, zoomResult.Data.password, request.StartNow);
                        if (!result.IsSuccess)
                        {
                            await transactionScope.RollbackAsync();

                        }
                        await transactionScope.CommitAsync();
                        return new ResponseModel<int>
                        {
                            HttpStatusCode = result.ResponseCode.GetStatusCode(),
                            MessageCode = result.Message,
                            IsSuccess = result.IsSuccess,
                            Result = result.Data
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
}
