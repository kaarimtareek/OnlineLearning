using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetRoomsByInterestsQueryHandler : IRequestHandler<GetRoomsByInterestsQuery, ResponseModel<PagedList<RoomDto>>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetRoomsByInterestsQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }
        public async Task<ResponseModel<PagedList<RoomDto>>> Handle(GetRoomsByInterestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using (AppDbContext context = new AppDbContext(dbContextOptions))
                {
                    var rooms = context.RoomInterests.Include(r => r.Room).ThenInclude(x => x.Status).Include(x => x.Room).ThenInclude(x => x.Owner).Include(x=>x.Room).ThenInclude(x=>x.RequestedUsers.Where(x=>x.UserId == request.UserId)).ThenInclude(x=>x.Status).AsNoTracking().Where(x => request.Interests.Contains(x.InterestId) && !x.IsDeleted && !x.Room.IsDeleted).OrderByDescending(x => x.Room.StartDate).Select(x => x.Room
                   ).Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize).Select(room => new RoomDto
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
                       UserRoomStatus = room.RequestedUsers.FirstOrDefault(x => x.UserId == request.UserId) ==null? null : new UserRoomStatusDto
                       {
                           Id = room.RequestedUsers.First(x => x.UserId == request.UserId).Status.Id,
                           NameArabic = room.RequestedUsers.First(x => x.UserId == request.UserId).Status.NameArabic,
                           NameEnglish = room.RequestedUsers.First(x => x.UserId == request.UserId).Status.NameEnglish,

                       }

                   });
                    var pagedList = await PagedList<RoomDto>.ToPagedList(rooms, request.PageNumber, request.PageSize);
                    return new ResponseModel<PagedList<RoomDto>>
                    {
                        HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                        IsSuccess = true,
                        MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                        Result = pagedList,
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
