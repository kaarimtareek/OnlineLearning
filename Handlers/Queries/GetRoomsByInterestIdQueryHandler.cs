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
                    var rooms = await context.Rooms.AsNoTracking().IncludeOwner().IncludeInterests().IncludeStatus().IsNotDeleted().Where(x => x.RoomInterests.Any(a => a.InterestId == request.InterestId && !a.IsDeleted)).OrderByDescending(x => x.StartDate).Select(x => new RoomDto
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
                        },
                        Interests = x.RoomInterests.Select(i => new InterestDto
                        {
                            Id = i.InterestId,
                            IsDeleted = i.IsDeleted

                        })
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
