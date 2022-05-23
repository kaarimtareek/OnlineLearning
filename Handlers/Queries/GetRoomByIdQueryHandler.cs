using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using OnlineLearning.Services;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, ResponseModel<RoomDto>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        private readonly IMapper mapper;

        public GetRoomByIdQueryHandler(DbContextOptions<AppDbContext> dbContextOptions, IMapper mapper)
        {
            this.dbContextOptions = dbContextOptions;
            this.mapper = mapper;
        }

        public async Task<ResponseModel<RoomDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            using (AppDbContext context = new AppDbContext(dbContextOptions))
            {
                var room = await context.Rooms.IsNotDeleted().IncludeOwner().IncludeInterests().IncludeUserRoomStatus(request.UserId).Where(x => x.Id == request.RoomId && !x.IsDeleted).Select( room => new RoomDto
                {
                    Id = room.Id,
                    Description = room.Description,
                    ExpectedEndDate = room.ExpectedEndDate,
                    FinishDate = room.FinishDate,
                    Interests = room.RoomInterests.Select(x => new InterestDto
                    {
                        Id = x.InterestId,
                        IsDeleted = x.IsDeleted
                    }).ToList(),
                    OwnerId = room.OwnerId,
                    Name = room.Name,
                    OwnerName = room.Owner.Name,
                    Price = room.Price,
                    StartDate = room.StartDate,
                    StatusId = room.StatusId,
                    IsPublic = room.IsPublic,
                    UserRoomStatus = room.RequestedUsers.FirstOrDefault()==null ? null : new UserRoomStatusDto
                    {
                        Id = room.RequestedUsers.First().StatusId,
                        NameArabic = room.RequestedUsers.First().Status.NameArabic,
                        NameEnglish = room.RequestedUsers.First().Status.NameEnglish,
                    },
                    Status = room.Status==null ? null : new RoomStatusDto
                    {
                        Id = room.Status.Id,
                        IsDeleted = room.Status.IsDeleted,
                        NameArabic = room.Status.NameArabic,
                        NameEnglish = room.Status.NameEnglish,
                    },
                }).FirstOrDefaultAsync();
                if (room ==null)
                {
                    return new ResponseModel<RoomDto>
                    {
                        IsSuccess = false,
                        HttpStatusCode = ResponseCodeEnum.NOT_FOUND.GetStatusCode(),
                        MessageCode = ConstantMessageCodes.ROOM_NOT_FOUND,
                    };
                }
               // var roomDto = mapper.Map<RoomDto>(room);
                return new ResponseModel<RoomDto>
                {
                    IsSuccess = true,
                    HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                    MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                    Result = room

                };
            }
        }
    }
}
