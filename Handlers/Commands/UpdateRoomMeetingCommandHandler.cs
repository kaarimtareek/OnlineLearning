using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Services;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Commands
{
    public class UpdateRoomMeetingCommandHandler : IRequestHandler<UpdateRoomMeetingCommand, ResponseModel<int>>

    {
        private readonly IZoomService zoomService;
        private readonly IMeetingService meetingService;
        private readonly DbContextOptions<AppDbContext> contextOptions;

        public UpdateRoomMeetingCommandHandler(IZoomService zoomService, IMeetingService meetingService, DbContextOptions<AppDbContext> contextOptions)
        {
            this.meetingService = meetingService;
            this.zoomService = zoomService;
            this.contextOptions = contextOptions;
        }

        public async Task<ResponseModel<int>> Handle(UpdateRoomMeetingCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                //if we need to call more than one function , and one of them failed , use rollback to undo all the changes
                using var transactionScope = await context.Database.BeginTransactionAsync();
                {
                    try
                    {
                        var roomMeeting = await context.RoomMeetings.Where(x => x.Id == request.MeetingId && !x.IsDeleted).FirstOrDefaultAsync();
                        if (roomMeeting == null)
                        {
                            return new ResponseModel<int>
                            {
                                IsSuccess = false,
                                MessageCode = ConstantMessageCodes.NOT_FOUND,
                                HttpStatusCode = ResponseCodeEnum.NOT_FOUND.GetStatusCode()
                            };
                        }
                        var zoomResult = await zoomService.UpdateZoomMeeting(request.ZoomToken, roomMeeting.ZoomMeetingId, new Models.NetworkModels.UpsertZoomMeetingRequest
                        {
                            agenda = request.MeetingName,
                            default_password =true,
                            duration = request.Duration,
                            start_time = request.StartDate,
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
                        var result = await meetingService.UpdateMeeting(context, roomMeeting, request.MeetingName, request.MeetingDescription, request.StartNow, request.StartDate, request.EndDate);
                        if (!result.IsSuccess)
                        {
                            await transactionScope.RollbackAsync();
                            return new ResponseModel<int>
                            {
                                HttpStatusCode = result.ResponseCode.GetStatusCode(),
                                MessageCode = result.Message,
                                IsSuccess = result.IsSuccess,
                                Result = result.Data
                            };
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
