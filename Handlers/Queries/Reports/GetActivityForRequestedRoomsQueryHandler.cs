using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs.Reports;
using OnlineLearning.Models;
using OnlineLearning.Queries.Reports;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries.Reports
{
    public class GetActivityForRequestedRoomsQueryHandler : IRequestHandler<GetActivityForRequestedRoomsQuery, ResponseModel<RequestedRoomsActivityDto>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetActivityForRequestedRoomsQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }

        public async Task<ResponseModel<RequestedRoomsActivityDto>> Handle(GetActivityForRequestedRoomsQuery request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
                RequestedRoomsActivityDto result = new RequestedRoomsActivityDto { 
                    UserId = request.UserId,
                    UserName = user.UserName,
                    RejectedRooms = new List<RequestedRoomDto>(),
                    LeftRooms = new List<RequestedRoomDto>(),
                    JoinedRooms = new List<RequestedRoomDto>(),
                    RequestedRooms = new List<RequestedRoomDto>()
                };
                var userRooms = await context.UsersRooms.Include(x => x.Room).ThenInclude(x=>x.Owner).AsNoTracking().Where(x => x.UserId == request.UserId && !x.IsDeleted).ToListAsync();
                
                foreach (var item in userRooms)
                {
                    switch (item.StatusId)
                    {
                        case Constants.ConstantUserRoomStatus.JOINED:
                            result.JoinedRooms.Add( new RequestedRoomDto { FinalDate = item.UpdatedAt, Id = item.RoomId, OwnerId = item.Room.OwnerId, OwnerName = item.Room.Owner.Name, RequestDate = item.CreatedAt, StatusId = item.StatusId });
                            break;
                        case Constants.ConstantUserRoomStatus.ACCEPTED:
                            result.JoinedRooms.Add(new RequestedRoomDto { FinalDate = item.UpdatedAt, Id = item.RoomId, OwnerId = item.Room.OwnerId, OwnerName = item.Room.Owner.Name, RequestDate = item.CreatedAt, StatusId = item.StatusId });
                            break;
                        case Constants.ConstantUserRoomStatus.LEFT:
                            result.LeftRooms.Add(new RequestedRoomDto { FinalDate = item.UpdatedAt, Id = item.RoomId, OwnerId = item.Room.OwnerId, OwnerName = item.Room.Owner.Name, RequestDate = item.CreatedAt, StatusId = item.StatusId });
                            break;
                        case Constants.ConstantUserRoomStatus.CANCELED:
                            result.LeftRooms.Add(new RequestedRoomDto { FinalDate = item.UpdatedAt, Id = item.RoomId, OwnerId = item.Room.OwnerId, OwnerName = item.Room.Owner.Name, RequestDate = item.CreatedAt, StatusId = item.StatusId });
                            break;
                        case Constants.ConstantUserRoomStatus.REJECTED:
                            result.RejectedRooms.Add(new RequestedRoomDto { FinalDate = item.UpdatedAt, Id = item.RoomId, OwnerId = item.Room.OwnerId, OwnerName = item.Room.Owner.Name, RequestDate = item.CreatedAt, StatusId = item.StatusId });
                            break;
                        case Constants.ConstantUserRoomStatus.SUSPENDED:
                            result.RejectedRooms.Add(new RequestedRoomDto { FinalDate = item.UpdatedAt, Id = item.RoomId, OwnerId = item.Room.OwnerId, OwnerName = item.Room.Owner.Name, RequestDate = item.CreatedAt, StatusId = item.StatusId });
                            break;
                        default:
                            break;
                    }
                }
                return ResponseModel.Success(ConstantMessageCodes.OPERATION_SUCCESS, result);
            }
        }
    }
}
