using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Common;
using OnlineLearning.Models.OutputModels;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using OnlineLearning.DTOs;
using System.Threading.Tasks;
using System.Threading;
using OnlineLearning.Services;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using OnlineLearning.Constants;
using static Humanizer.In;

namespace OnlineLearning.Handlers.Queries
{
    public class GetCreatedRoomsQueryHandler : IRequestHandler<GetCreatedRoomsQuery, ResponseModel<PagedList<RoomDto>>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        private readonly IMapper mapper;

        public GetCreatedRoomsQueryHandler(DbContextOptions<AppDbContext> dbContextOptions, IMapper mapper)
        {
            this.dbContextOptions=dbContextOptions;
            this.mapper=mapper;
        }

        public async Task<ResponseModel<PagedList<RoomDto>>> Handle(GetCreatedRoomsQuery request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                var rooms = await context.Rooms.IsNotDeleted().IncludeOwner().IncludeInterests().IncludeUserRoomStatus(request.UserId).AsNoTracking().Where(x => x.OwnerId == request.UserId && !x.IsDeleted).Select(room=> new RoomDto
                {
                    Id = room.Id,
                    Description = room.Description,
                    ExpectedEndDate = room.ExpectedEndDate,
                    FinishDate = room.FinishDate,
                    NumberOfJoinedUsers = room.NumberOfJoinedUsers,
                    NumberOfLeftUsers = room.NumberOfLeftUsers,
                    NumberOfRejectedUsers = room.NumberOfRejectedUsers,
                    NumberOfRequestedUsers = room.NumberOfRequestedUsers,
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
                    UserRoomStatus = room.RequestedUsers.FirstOrDefault()==null? null: new UserRoomStatusDto
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
                }).ToListAsync();
                var roomsDto = mapper.Map<List<RoomDto>>(rooms);
                return new ResponseModel<PagedList<RoomDto>>
                {
                    HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                    IsSuccess = true,
                    MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                    Result = new PagedList<RoomDto>(rooms, rooms.Count, 1, rooms.Count)
                };
            }
        }
    }
}
