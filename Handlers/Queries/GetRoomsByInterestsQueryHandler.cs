using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;

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
                using(AppDbContext context = new AppDbContext(dbContextOptions))
                {
                    var rooms = context.RoomInterests.Include(r => r.Room).ThenInclude(x => x.Status).Include(x => x.Room).ThenInclude(x => x.Owner).AsNoTracking().Where(x => request.Interests.Contains(x.InterestId) && !x.IsDeleted && !x.Room.IsDeleted).OrderByDescending(x => x.Room.StartDate).Select(x => x.Room
                   ).Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize).Select(x => new RoomDto
                   {
                       Description = x.Description,
                       ExpectedEndDate = x.ExpectedEndDate,
                       FinishDate = x.FinishDate,
                       Id = x.Id,
                       IsPublic = x.IsPublic,
                       Name = x.Name,
                       OwnerId = x.OwnerId,
                       OwnerName = x.Owner.Name,
                       Price = x.Price,
                       StartDate = x.StartDate,
                       StatusId = x.StatusId,
                       Status = x.Status == null ? null : new RoomStatusDto
                       {
                           Id = x.Status.Id,
                           NameArabic = x.Status.NameArabic,
                           NameEnglish = x.Status.NameEnglish,
                           IsDeleted = x.Status.IsDeleted
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
            catch(Exception e)
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
