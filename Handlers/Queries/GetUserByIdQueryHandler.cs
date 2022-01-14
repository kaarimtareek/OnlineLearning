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
using OnlineLearning.Services;

namespace OnlineLearning.Handlers.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseModel<UserDto>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetUserByIdQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task<ResponseModel<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                var user = await context.ApplicationUsers
                                   .IsNotDeleted()
                                   .GetCreatedRoomsForUser()
                                   .GetAllRoomsForUser()
                                   .GetUserInterests()
                                   .Select(x => new UserDto
                                   {
                                       Id = request.Id,
                                       JoinedRooms = x.RequestedRooms.Where(x => x.StatusId == ConstantUserRoomStatus.JOINED).Select(r => new RoomDto
                                       {
                                           Description = r.Room.Description,
                                           Id = r.Room.Id,
                                           ExpectedEndDate = r.Room.ExpectedEndDate,
                                           FinishDate = r.Room.FinishDate,
                                           Name = r.Room.Name,
                                           OwnerId = r.Room.OwnerId,
                                           Price = r.Room.Price,
                                           StatusId = r.Room.StatusId,
                                           StartDate = r.Room.StartDate,
                                           IsPublic = r.Room.IsPublic,
                                       }).ToList(),
                                       StatusId = x.StatusId,
                                       UserName = x.UserName,
                                       Name = x.Name,
                                       CreatedAt = x.CreatedAt,
                                       Birthdate = x.Birthdate,
                                       PhoneNumber = x.PhoneNumber,
                                       Status = x.Status == null ? null : new UserStatusDto
                                       {
                                           Id = x.Status.Id,
                                           NameArabic = x.Status.NameArabic,
                                           NameEnglish = x.Status.NameEnglish,
                                       },
                                       CreatedRooms = x.CreatedRooms.Select(r => new RoomDto
                                       {
                                           Id = r.Id,
                                           Description = r.Description,
                                           ExpectedEndDate = r.ExpectedEndDate,
                                           FinishDate = r.FinishDate,
                                           Name = r.Name,
                                           Price = r.Price,
                                           OwnerId = r.OwnerId,
                                           StatusId = r.StatusId,
                                           StartDate = r.StartDate,
                                           IsPublic = r.IsPublic,
                                           
                                       }).ToList(),
                                       Interests = x.UserInterests.Select(r => new InterestDto
                                       {
                                           Id = r.InterestId,
                                           IsDeleted = r.IsDeleted
                                       }).ToList(),
                                   })
                                   .FirstOrDefaultAsync(x => x.Id == request.Id);

                return user == null
                    ? new ResponseModel<UserDto>
                    {
                        IsSuccess = false,
                        HttpStatusCode = ResponseCodeEnum.FAILED.GetStatusCode(),
                        Result = null,
                        MessageCode = ConstantMessageCodes.USER_NOT_FOUND,
                    }
                    : new ResponseModel<UserDto>
                {
                    IsSuccess = true,
                    HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                    Result = user,
                    MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                };
            }
        }
    }
}