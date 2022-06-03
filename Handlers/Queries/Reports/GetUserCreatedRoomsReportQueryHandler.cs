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
    public class GetUserCreatedRoomsReportQueryHandler : IRequestHandler<GetUserCreatedRoomsReportQuery, ResponseModel<UserCreatedRoomsActivityDto>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetUserCreatedRoomsReportQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }

        public async Task<ResponseModel<UserCreatedRoomsActivityDto>> Handle(GetUserCreatedRoomsReportQuery request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                bool skipFrom = request.From == null;
                bool skipTo = request.To == null;
                bool skipStatus = request.Statusess == null || request.Statusess.Length == 0;

                var result = new UserCreatedRoomsActivityDto { UserId = request.UserId, RoomActivities = new List<RoomActivityDto>() };
                var roomIds = await context.Rooms.AsNoTracking().Where(x => x.OwnerId == request.UserId && (skipFrom || x.CreatedAt >= request.From.Value ) && (skipTo || x.CreatedAt <= request.To) && (skipStatus || request.Statusess.Contains(x.StatusId))).Select(x => x.Id).ToListAsync();
                foreach (var roomId in roomIds)
                {
                    var room = await context.Rooms.Include(x => x.RoomMaterials).Include(x => x.RequestedUsers).ThenInclude(x => x.User).Where(x => x.Id == roomId && !x.IsDeleted).Select(x => new RoomActivityDto
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
                        JoinedUserRoomActivities = x.RequestedUsers.Where(x => x.StatusId == Constants.ConstantUserRoomStatus.JOINED || x.StatusId == Constants.ConstantUserRoomStatus.ACCEPTED).Select(ur => new UserRoomActivityDto
                        {
                            Id = ur.Id,
                            CreatedAt = ur.CreatedAt,
                            StatusId = ur.StatusId,
                            UserId = ur.UserId,
                            UserName = ur.User.Name,
                            UserRoomHistories = ur.UserRoomHistories.Select(urh => new UserRoomHistoryDto
                            {
                                Comment = urh.Comment,
                                CreatedAt = urh.CreatedAt,
                                StatusId=urh.StatusId,
                                LeaveReason = urh.LeaveReason,
                                RejectionReason = urh.RejectionReason,
                                RoomId = urh.RoomId,
                                SuspensionReason = urh.SuspensionReason,
                                UserId = urh.UserId,
                                UserName = urh.User.Name,
                                UserRoomActivityId = urh.UserRoomsId
                            }).ToList()
                        }).ToList(),
                        LeftUserRoomActivities =  x.RequestedUsers.Where(x => x.StatusId == Constants.ConstantUserRoomStatus.LEFT || x.StatusId == Constants.ConstantUserRoomStatus.CANCELED).Select(x => new UserRoomActivityDto
                        {
                            Id = x.Id,
                            CreatedAt = x.CreatedAt,
                            StatusId = x.StatusId,
                            UserId = x.UserId,
                            UserName = x.User.Name,
                            UserRoomHistories = x.UserRoomHistories.Select(x => new UserRoomHistoryDto
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
                            }).ToList()
                        }).ToList(),
                        RejectedUserRoomActivities =  x.RequestedUsers.Where(x => x.StatusId == Constants.ConstantUserRoomStatus.REJECTED).Select(x => new UserRoomActivityDto
                        {
                            Id = x.Id,
                            CreatedAt = x.CreatedAt,
                            StatusId = x.StatusId,
                            UserId = x.UserId,
                            UserName = x.User.Name,
                            UserRoomHistories = x.UserRoomHistories.Select(x => new UserRoomHistoryDto
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
                            }).ToList()
                        }).ToList(),
                        RequestedUserRoomActivities =  x.RequestedUsers.Where(x => x.StatusId == Constants.ConstantUserRoomStatus.PENDING).Select(x => new UserRoomActivityDto
                        {
                            Id = x.Id,
                            CreatedAt = x.CreatedAt,
                            StatusId = x.StatusId,
                            UserId = x.UserId,
                            UserName = x.User.Name,
                            UserRoomHistories = x.UserRoomHistories.Select(x => new UserRoomHistoryDto
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
                            }).ToList()
                        }).ToList(),
                        RoomMaterialsActivities = x.RoomMaterials.Select(x => new RoomMaterialActivityDto
                        {
                            CreatedAt = x.CreatedAt,
                            MaterialId = x.Id,
                            MaterialName = x.FileName
                        }).ToList(),

                    }).FirstOrDefaultAsync(cancellationToken);
                    var userRoomsHistories = await context.UserRoomsHistories.Include(x => x.User).AsNoTracking().Where(x => x.RoomId == roomId).Select(x => new UserRoomHistoryDto
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
                    }).ToListAsync(cancellationToken);
                    var roomMeetings = await context.RoomMeetings.AsNoTracking().Where(x => x.RoomId == roomId).Select(x => new RoomMeetingActivityDto
                    {
                        EndDate = x.EndDate,
                        MeetingId =x.Id,
                        StartDate = x.StartDate,
                        TopicName = x.MeetingName,
                        RoomId = x.RoomId,
                        CreatedAt = x.CreatedAt,

                    }).ToListAsync(cancellationToken);
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
                    result.RoomActivities.Add(room);
                }
                return ResponseModel.Success(ConstantMessageCodes.OPERATION_SUCCESS, result);
            }
        }
    }
}
