using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using OnlineLearning.Services;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetRoomsByInterestIdQueryHandler : IRequestHandler<GetRoomsByInterestIdQuery, ResponseModel<PagedList<RoomDto>>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetRoomsByInterestIdQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }
        public async Task<ResponseModel<PagedList<RoomDto>>> Handle(GetRoomsByInterestIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using (AppDbContext context = new AppDbContext(dbContextOptions))
                {
                    var rooms = await context.Rooms.AsNoTracking().IncludeOwner().IncludeInterests().IncludeStatus().IncludeUserRoomStatus(request.UserId).IsNotDeleted().Where(x => x.RoomInterests.Any(a => a.InterestId == request.InterestId && !a.IsDeleted)).OrderByDescending(x => x.StartDate).Select(room => new RoomDto
                    {
                        Description = room.Description,
                        ExpectedEndDate = room.ExpectedEndDate,
                        FinishDate = room.FinishDate,
                        Id = room.Id,
                        IsPublic = room.IsPublic,
                        Name = room.Name,
                        OwnerId = room.OwnerId,
                        OwnerName = room.Owner.Name,
                        Price = room.Price,
                        StartDate = room.StartDate,
                        StatusId = room.StatusId,
                        NumberOfJoinedUsers = room.NumberOfJoinedUsers,
                        NumberOfLeftUsers = room.NumberOfLeftUsers,
                        NumberOfRejectedUsers = room.NumberOfRejectedUsers,
                        NumberOfRequestedUsers = room.NumberOfRequestedUsers,
                        Status = room.Status == null ? null : new RoomStatusDto
                        {
                            Id = room.Status.Id,
                            NameArabic = room.Status.NameArabic,
                            NameEnglish = room.Status.NameEnglish,
                            IsDeleted = room.Status.IsDeleted
                        },
                        Interests = room.RoomInterests.Select(i => new InterestDto
                        {
                            Id = i.InterestId,
                            NumberOfInterestedUsers = i.Interest.NumberOfInterestedUsers,
                            IsDeleted = i.IsDeleted

                        }),
                        UserRoomStatus = room.RequestedUsers.FirstOrDefault(x => x.UserId == request.UserId) == null ? null : new UserRoomStatusDto
                        {
                            Id = room.RequestedUsers.First(x => x.UserId == request.UserId).StatusId,
                            NameArabic = room.RequestedUsers.First(x => x.UserId == request.UserId).Status.NameArabic,
                            NameEnglish = room.RequestedUsers.First(x => x.UserId == request.UserId).Status.NameEnglish,
                        }
                    }).ToPagedList(request.PageNumber, request.PageSize);
                    return new ResponseModel<PagedList<RoomDto>>
                    {
                        HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                        IsSuccess = true,
                        MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                        Result = rooms,
                    };
                }
            }
            catch (Exception e)
            {
                return new ResponseModel<PagedList<RoomDto>>
                {
                    HttpStatusCode = ResponseCodeEnum.FAILED.GetStatusCode(),
                    IsSuccess = false,
                    MessageCode = ConstantMessageCodes.OPERATION_FAILED,
                };
            }
        }
    }
}
