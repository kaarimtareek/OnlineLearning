using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Common;
using OnlineLearning.Models.OutputModels;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using OnlineLearning.Queries.Reports;
using OnlineLearning.DTOs.Reports;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using OnlineLearning.Constants;

namespace OnlineLearning.Handlers.Queries.Reports
{
    public class GetRoomActivityQueryHandler : IRequestHandler<GetActivityForRoomQuery, ResponseModel<RoomActivityDto>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;
        public GetRoomActivityQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task<ResponseModel<RoomActivityDto>> Handle(GetActivityForRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                using (var context = new AppDbContext(dbContextOptions))
                {

                    var room = await context.Rooms.Include(x => x.RequestedUsers).ThenInclude(x => x.User).Where(x => x.Id == request.RoomId && !x.IsDeleted).Select(x => new RoomActivityDto
                    {
                        RoomId = x.Id,
                        Description = x.Description,
                        ExpectedEndDate = x.ExpectedEndDate,
                        FinishDate = x.FinishDate,
                        IsPublic = x.IsPublic,
                        NumberOfJoinedUsers = x.NumberOfJoinedUsers,
                        NumberOfLeftUsers = x.NumberOfLeftUsers,
                        NumberOfRejectedUsers = x.NumberOfRejectedUsers,
                        NumberOfRequestedUsers = x.NumberOfRequestedUsers,
                        RoomName = x.Name,
                        StartDate = x.StartDate,
                        StatusId = x.StatusId,
                        Price = x.Price,
                    }).FirstOrDefaultAsync();
                    var requestedUsers = await context.UsersRooms.Include(x=>x.User).AsNoTracking().Where(x => x.RoomId == request.RoomId && !x.IsDeleted).Select(x=> new UserRoomActivityDto
                    {
                        CreatedAt = x.CreatedAt,
                        Id = x.Id,
                        StatusId = x.StatusId,
                        UserId = x.UserId,
                        UserName = x.User.Name,
                        

                    }).ToListAsync();
                    room.RejectedUserRoomActivities = requestedUsers.Where(x => x.StatusId == ConstantUserRoomStatus.REJECTED).ToList();
                    room.RequestedUserRoomActivities = requestedUsers.Where(x => x.StatusId == ConstantUserRoomStatus.PENDING).ToList();
                    room.LeftUserRoomActivities = requestedUsers.Where(x => x.StatusId == Constants.ConstantUserRoomStatus.LEFT || x.StatusId == Constants.ConstantUserRoomStatus.CANCELED).ToList();
                    room.JoinedUserRoomActivities = requestedUsers.Where(x => x.StatusId == Constants.ConstantUserRoomStatus.JOINED || x.StatusId == Constants.ConstantUserRoomStatus.ACCEPTED).ToList();
                    room.RoomMaterialsActivities = await context.RoomMaterials.AsNoTracking().Where(x => x.RoomId == request.RoomId && !x.IsDeleted).Select(x=> new RoomMaterialActivityDto { MaterialId = x.Id,CreatedAt = x.CreatedAt, MaterialName = x.FileName}).ToListAsync();
                    var userRoomsHistories = await context.UserRoomsHistories.Include(x => x.User).AsNoTracking().Where(x => x.RoomId == request.RoomId).Select(x => new UserRoomHistoryDto
                    {
                        Comment = x.Comment,
                        CreatedAt = x.CreatedAt,
                        StatusId=x.StatusId,
                        LeaveReason = x.LeaveReason,
                        RejectionReason = x.RejectionReason,
                        RoomId = x.RoomId,
                        SuspensionReason = x.SuspensionReason,
                        UserId = x.UserId,
                        UserName = x.User.Name,
                        UserRoomActivityId = x.UserRoomsId
                    }).ToListAsync();
                    var roomMeetings = await context.RoomMeetings.AsNoTracking().Where(x => x.RoomId == request.RoomId).Select(x => new RoomMeetingActivityDto
                    {
                        EndDate = x.EndDate,
                        MeetingId =x.Id,
                        StartDate = x.StartDate,
                        TopicName = x.MeetingName,
                        RoomId = x.RoomId,
                        CreatedAt = x.CreatedAt,

                    }).ToListAsync();
                    room.RoomMeetingActivities = roomMeetings;
                    foreach (var item in userRoomsHistories)
                    {
                        foreach (var joinedRoomH in room.JoinedUserRoomActivities)
                        {
                            if (joinedRoomH.UserRoomHistories == null)
                                joinedRoomH.UserRoomHistories = new System.Collections.Generic.List<UserRoomHistoryDto>();
                            if (joinedRoomH.Id == item.UserRoomActivityId)
                            {
                                joinedRoomH.UserRoomHistories.Add(item);
                            }
                        }
                        foreach (var joinedRoomH in room.RequestedUserRoomActivities)
                        {
                            if (joinedRoomH.UserRoomHistories == null)
                                joinedRoomH.UserRoomHistories = new System.Collections.Generic.List<UserRoomHistoryDto>();
                            if (joinedRoomH.Id == item.UserRoomActivityId)
                            {
                                joinedRoomH.UserRoomHistories.Add(item);
                            }
                        }
                        foreach (var joinedRoomH in room.RejectedUserRoomActivities)
                        {
                            if (joinedRoomH.UserRoomHistories == null)
                                joinedRoomH.UserRoomHistories = new System.Collections.Generic.List<UserRoomHistoryDto>();
                            if (joinedRoomH.Id == item.UserRoomActivityId)
                            {
                                joinedRoomH.UserRoomHistories.Add(item);
                            }
                        }
                        foreach (var joinedRoomH in room.LeftUserRoomActivities)
                        {
                            if (joinedRoomH.UserRoomHistories == null)
                                joinedRoomH.UserRoomHistories = new System.Collections.Generic.List<UserRoomHistoryDto>();
                            if (joinedRoomH.Id == item.UserRoomActivityId)
                            {
                                joinedRoomH.UserRoomHistories.Add(item);
                            }
                        }

                    }
                    return ResponseModel.Success(Constants.ConstantMessageCodes.OPERATION_SUCCESS, room);

                }
            }
            catch(Exception e)
            {
                return ResponseModel.Fail<RoomActivityDto>(e.ToString());
            }
        }
    }
}
