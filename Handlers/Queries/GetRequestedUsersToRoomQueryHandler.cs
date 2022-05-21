using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetRequestedUsersToRoomQueryHandler : IRequestHandler<GetRequestedUsersToRoomQuery, ResponseModel<List<UserDto>>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetRequestedUsersToRoomQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task<ResponseModel<List<UserDto>>> Handle(GetRequestedUsersToRoomQuery request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                var isUserIsTheOwner = await context.Rooms.AnyAsync(x => x.Id == request.RoomId && x.OwnerId == request.RoomOwnerId && !x.IsDeleted);
                if (!isUserIsTheOwner)
                {
                    return new ResponseModel<List<UserDto>>
                    {
                        IsSuccess = true,
                        MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                        HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                    };
                }
                var users = await context.UsersRooms.Include(x => x.User).AsNoTracking().Where(x => x.RoomId == request.RoomId && x.StatusId == ConstantUserRoomStatus.PENDING && !x.IsDeleted && x.User.StatusId == ConstantUserStatus.ACTIVE).Select(x => new UserDto
                {
                    IsDeleted = false,
                    Id = x.User.Id,
                    StatusId = x.User.StatusId,
                    Birthdate = x.User.Birthdate,
                    Name = x.User.Name,
                    PhoneNumber = x.User.PhoneNumber,
                }).ToListAsync();
                return new ResponseModel<List<UserDto>>
                {
                    IsSuccess = true,
                    MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                    Result = users,
                    HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                };
            }
        }
    }
}