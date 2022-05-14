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
    public class DeleteRoomMeetingCommandHandler : IRequestHandler<DeleteRoomMeetingCommand, ResponseModel<int>>

    {
        private readonly IZoomService zoomService;
        private readonly IMeetingService meetingService;
        private readonly DbContextOptions<AppDbContext> contextOptions;

        public DeleteRoomMeetingCommandHandler(IZoomService zoomService, IMeetingService meetingService, DbContextOptions<AppDbContext> contextOptions)
        {
            this.meetingService = meetingService;
            this.zoomService = zoomService;
            this.contextOptions = contextOptions;
        }

        public async Task<ResponseModel<int>> Handle(DeleteRoomMeetingCommand request, CancellationToken cancellationToken)
        {
			using (var context = new AppDbContext(contextOptions))
			{
				//if we need to call more than one function , and one of them failed , use rollback to undo all the changes
				using var transactionScope = await context.Database.BeginTransactionAsync();
				{
					try
					{
						var roomMeeting = await context.RoomMeetings.Where(x => x.Id == request.MeetingId && !x.IsDeleted).FirstOrDefaultAsync();
						if(roomMeeting == null)
                        {
							return new ResponseModel<int>
							{
								IsSuccess = false,
								MessageCode = ConstantMessageCodes.NOT_FOUND,
								HttpStatusCode = ResponseCodeEnum.NOT_FOUND.GetStatusCode()
							};
						}
						var zoomResult = await zoomService.DeleteZoomMeeting(request.ZoomToken,roomMeeting.ZoomMeetingId);

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
						var result = await meetingService.DeleteMeeting(context,roomMeeting);
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
