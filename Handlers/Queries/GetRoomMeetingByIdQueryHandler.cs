using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetRoomMeetingByIdQueryHandler : IRequestHandler<GetRoomMeetingByIdQuery, ResponseModel<RoomMeetingDto>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetRoomMeetingByIdQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }

        public async Task<ResponseModel<RoomMeetingDto>> Handle(GetRoomMeetingByIdQuery request, CancellationToken cancellationToken)
        {
            using (AppDbContext context = new AppDbContext(dbContextOptions))
            {
                var roomMeetingDto = await context.RoomMeetings.Include(x => x.Status).AsNoTracking().Where(x => x.Id == request.RoomMeetingId && !x.IsDeleted).Select(x => new RoomMeetingDto
                {
                    Id = request.RoomMeetingId,
                    MeetingName = x.MeetingName,
                    MeetingDescription = x.MeetingDescription,
                    EndDate = x.EndDate,
                    Duration = x.Duration,
                    MeetingPassword = x.MeetingPassword,
                    OwnerId = x.OwnerId,
                    StartDate = x.StartDate,
                    Status = new RoomMeetingStatusDto
                    {
                        Id = x.Status.Id,
                        IsDeleted = x.Status.IsDeleted,
                        NameArabic = x.Status.NameArabic,
                        NameEnglish =  x.Status.NameEnglish
                    },
                    StatusId = x.Status.Id,
                    MeetingUrl = x.MeetingUrl,
                    RoomId = x.RoomId,
                    ZoomMeetingId = x.ZoomMeetingId
                }).FirstOrDefaultAsync();
                if (roomMeetingDto == null)
                {
                    return new ResponseModel<RoomMeetingDto>
                    {
                        HttpStatusCode = ResponseCodeEnum.NOT_FOUND.GetStatusCode(),
                        IsSuccess = false,
                        MessageCode = ConstantMessageCodes.NOT_FOUND,

                    };
                }
                return new ResponseModel<RoomMeetingDto>
                {
                    HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                    IsSuccess = true,
                    MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                    Result = roomMeetingDto
                };
            }
        }
    }
}
