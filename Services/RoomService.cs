using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public class RoomService : IRoomService
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly ILoggerService<RoomService> logger;
        private readonly IFileManager fileManager;

        public RoomService(DbContextOptions<AppDbContext> contextOptions, ILoggerService<RoomService> logger, IFileManager fileManager)
        {
            this.contextOptions = contextOptions;
            this.logger = logger;
            this.fileManager = fileManager;
        }

        public async Task<OperationResult<Room>> CreateRoom(AppDbContext context, string userId, string roomName, string roomDescription, decimal price, DateTime StartDate, DateTime? expectedEndDate, bool isPublic, List<string> interests)
        {
            try
            {
                var roomStatus = await context.LookupRoomStatuses.AsNoTracking().ToListAsync();
                var room = new Room
                {
                    Name = roomName,
                    OwnerId = userId,
                    Price = price,
                    StartDate = StartDate,
                    ExpectedEndDate = expectedEndDate,
                    Description = roomDescription,
                    StatusId = ConstantRoomStatus.PENDING,
                    IsPublic = isPublic
                };
                //adding room interests
                var roomInterests = interests.ConvertAll(x => new RoomInterest
                {
                    InterestId = x,
                    Room = room,
                });
                await context.Rooms.AddAsync(room);
                await context.RoomInterests.AddRangeAsync(roomInterests);
                await context.SaveChangesAsync();
                return new OperationResult<Room>
                {
                    IsSuccess = true,
                    Message = ConstantMessageCodes.OPERATION_SUCCESS,
                    ResponseCode = ResponseCodeEnum.SUCCESS,
                    Data = room,
                };
            }
            catch (Exception e)
            {
                logger.LogError($"error in create room {e}");
                return new OperationResult<Room>
                {
                    IsSuccess = false,
                    Message = ConstantMessageCodes.OPERATION_FAILED,
                    ResponseCode = ResponseCodeEnum.FAILED,
                };
            }
        }

        public async Task<OperationResult<Room>> GetRoomById(int roomId)
        {
            try
            {
                using var context = new AppDbContext(contextOptions);
                var room = await context.Rooms.Include(x => x.RoomInterests.Where(x => !x.IsDeleted)).ThenInclude(x => x.Interest).FirstOrDefaultAsync(x => x.Id == roomId && !x.IsDeleted);
                if (room == null)
                {
                    return new OperationResult<Room>
                    {
                        IsSuccess = false,
                        Message = ConstantMessageCodes.ROOM_NOT_FOUND,
                        ResponseCode = ResponseCodeEnum.NOT_FOUND,
                    };
                }
                return new OperationResult<Room>
                {
                    IsSuccess = true,
                    Data = room,
                    Message = ConstantMessageCodes.OPERATION_SUCCESS,
                    ResponseCode = ResponseCodeEnum.SUCCESS
                };
            }
            catch (Exception e)
            {
                logger.LogError($"error in GetRoomById {e}");
                return new OperationResult<Room>
                {
                    IsSuccess = false,
                    Message = ConstantMessageCodes.OPERATION_FAILED,
                    ResponseCode = ResponseCodeEnum.FAILED,
                };
            }
        }
        public async Task<OperationResult<int>> InviteUserToRoom(AppDbContext context, int roomId,string ownerId,string userId)
        {
            try
            {
                var room = await context.Rooms.Include(x=>x.RequestedUsers).FirstOrDefaultAsync(x => x.Id == roomId && !x.IsDeleted);
                if (room == null)
                {
                    return new OperationResult<int>
                    {
                        IsSuccess = false,
                        Message = ConstantMessageCodes.ROOM_NOT_FOUND,
                        ResponseCode = ResponseCodeEnum.NOT_FOUND,
                    };
                }
                if(room.RequestedUsers.Any(x=>x.UserId == userId && !x.IsDeleted))
                {
                    return OperationResult.Fail<int>(ConstantMessageCodes.ALREADY_REQUESTED_TO_ROOM, default, ResponseCodeEnum.BAD_INPUT);
                }
                var userInvite = await context.UserInvites.FirstOrDefaultAsync(x => x.UserId == userId && x.RoomId == roomId && !x.IsDeleted);
                if(userInvite == null)
                {
                    userInvite = new UserInvites
                    {
                        RoomId = roomId,
                        OwnerId = ownerId,
                        UserId = userId,
                        StatusId = ConstantUserInvites.PENDING
                    };
                    await context.UserInvites.AddAsync(userInvite);
                    await context.SaveChangesAsync();
                    await ChangeUserRoomStatus(context, userId, roomId, ConstantUserRoomStatus.INVITED, ConstantUserRoomStatus.RoomOwnerAllowedStatus);

                    return new OperationResult<int>
                    {
                        IsSuccess = true,
                        Data = userInvite.Id,
                        Message = ConstantMessageCodes.OPERATION_SUCCESS,
                        ResponseCode = ResponseCodeEnum.SUCCESS
                    };
                }
                return OperationResult.Fail<int>(ConstantMessageCodes.ALREADY_REQUESTED_TO_ROOM,default,ResponseCodeEnum.BAD_INPUT);
                
            }
            catch (Exception e)
            {
                logger.LogError($"error in GetRoomById {e}");
                return new OperationResult<int>
                {
                    IsSuccess = false,
                    Message = ConstantMessageCodes.OPERATION_FAILED,
                    ResponseCode = ResponseCodeEnum.FAILED,
                };
            }
        }
        public async Task<OperationResult<int>> ChangeInviteUser(AppDbContext context,int inviteId ,string status)
        {
            try
            {
                var userInvite = await context.UserInvites.Include(x=>x.Room).FirstOrDefaultAsync(x => x.Id == inviteId && !x.IsDeleted);
                if (userInvite == null)
                {
                    return new OperationResult<int>
                    {
                        IsSuccess = false,
                        Message = ConstantMessageCodes.NOT_FOUND,
                        ResponseCode = ResponseCodeEnum.NOT_FOUND,
                    };
                }
                if(userInvite.StatusId == status)
                    return OperationResult.Fail<int>(ConstantMessageCodes.ALREADY_REQUESTED_TO_ROOM,default,ResponseCodeEnum.BAD_INPUT);

                var userRoom = await context.UsersRooms.Include(x=>x.Room).FirstOrDefaultAsync(x => x.RoomId == userInvite.RoomId && x.UserId == userInvite.UserId && !x.IsDeleted);
                string oldStatus = string.Empty;
                if (userRoom != null)
                    oldStatus = userRoom.StatusId;
                userInvite.StatusId = status;
                string newStatus = GetUserRoomStatusFromInvite(status, userInvite.Room);
                await ChangeUserRoomStatus(context, userInvite.UserId, userInvite.RoomId, newStatus, ConstantUserRoomStatus.UserAllowedStatus);
                await UpdateNumberOfUsers(context, userInvite.RoomId, oldStatus, -1);
                await UpdateNumberOfUsers(context, userInvite.RoomId, newStatus);
                await context.SaveChangesAsync();
                return OperationResult.Success(userInvite.Id);
            }
            catch (Exception e)
            {
                logger.LogError($"error in GetRoomById {e}");
                return new OperationResult<int>
                {
                    IsSuccess = false,
                    Message = ConstantMessageCodes.OPERATION_FAILED,
                    ResponseCode = ResponseCodeEnum.FAILED,
                };
            }
        }
        public async Task<OperationResult<Room>> StartRoom(int roomId)
        {
            try
            {
                using var context = new AppDbContext(contextOptions);

                var room = await context.Rooms.Include(x=>x.RequestedUsers).FirstOrDefaultAsync(x => x.Id == roomId && !x.IsDeleted);
                if (room == null)
                {
                    return new OperationResult<Room>
                    {
                        IsSuccess = false,
                        Message = ConstantMessageCodes.ROOM_NOT_FOUND,
                        ResponseCode = ResponseCodeEnum.NOT_FOUND,
                    };
                }
                if (ConstantRoomStatus.IsActive(room.StatusId))
                    return OperationResult.Fail<Room>(ConstantMessageCodes.ROOM_ALREADY_ACTIVE,default,ResponseCodeEnum.DUPLICATE_DATA);
                room.StatusId = ConstantRoomStatus.ACTIVE;
                foreach (var item in room.RequestedUsers.Where(x=>x.StatusId == ConstantUserRoomStatus.PENDING || x.StatusId == ConstantUserRoomStatus.ACCEPTED))
                {
                    await ChangeUserRoomStatus(context, item.UserId, item.RoomId, ConstantUserRoomStatus.JOINED, ConstantUserRoomStatus.RoomOwnerAllowedStatus);
                    await UpdateNumberOfUsers(context, roomId, ConstantUserRoomStatus.JOINED);
                }
                await context.SaveChangesAsync();
                return OperationResult.Success(room);
            }
            catch (Exception e)
            {
                logger.LogError($"error in GetRoomById {e}");
                return new OperationResult<Room>
                {
                    IsSuccess = false,
                    Message = ConstantMessageCodes.OPERATION_FAILED,
                    ResponseCode = ResponseCodeEnum.FAILED,
                };
            }
        }

        public async Task<OperationResult<LookupRoomStatus>> GetLookupRoomStatusById(string id)
        {
            using var context = new AppDbContext(contextOptions);
            var status = await context.LookupRoomStatuses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (status == null)
            {
                return new OperationResult<LookupRoomStatus>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ConstantMessageCodes.ROOM_STATUS_NOT_FOUND,
                    ResponseCode = ResponseCodeEnum.NOT_FOUND
                };
            }
            return new OperationResult<LookupRoomStatus>
            {
                ResponseCode = ResponseCodeEnum.SUCCESS,
                Message = ConstantMessageCodes.OPERATION_SUCCESS,
                Data = status,
                IsSuccess = true
            };
        }

        public async Task<OperationResult<int>> RequestToJoinRoom(AppDbContext context, int roomId, string userId)
        {
            OperationResult<int> result = new OperationResult<int>();
            try
            {
                var room = await context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId && !x.IsDeleted);
                if (room == null)
                {
                    result.IsSuccess = false;
                    result.Message = ConstantMessageCodes.ROOM_NOT_FOUND;
                    result.ResponseCode = ResponseCodeEnum.NOT_FOUND;
                    return result;
                }
                //user can't join an owned room
                if (room.OwnerId == userId)
                {
                    result.IsSuccess = false;
                    result.Message = ConstantMessageCodes.CANT_JOIN_OWNED_ROOM;
                    result.ResponseCode = ResponseCodeEnum.BAD_INPUT;
                    return result;
                }
                if (!IsValidRoomToJoin(room))
                {
                    result.Message = ConstantMessageCodes.ROOM_NOT_VAlID_TO_JOIN;
                    result.ResponseCode = ResponseCodeEnum.INVALID_DATA;
                    return result;
                }
                if (await context.UsersRooms.AnyAsync(x => x.UserId == userId && x.RoomId == roomId && !x.IsDeleted))
                {
                    return OperationResult.Fail<int>(ConstantMessageCodes.ALREADY_REQUESTED_TO_ROOM,default,ResponseCodeEnum.BAD_INPUT);
                }
                UsersRooms userRoom = new UsersRooms
                {
                    RoomId = roomId,
                    UserId = userId,
                };
                userRoom.StatusId = GetStatusForUserRoom(room);
                var userRoomHistory = new UserRoomsHistory
                {
                    Comment = userRoom.Comment,
                    LeaveReason = userRoom.LeaveReason,
                    RejectionReason = userRoom.RejectionReason,
                    StatusId = userRoom.StatusId,
                    UserId = userId,
                    UserRoomsId = userRoom.Id,
                    SuspensionReason = userRoom.SuspensionReason,
                    RoomId=roomId,
                };
                await context.UserRoomsHistories.AddAsync(userRoomHistory);
                await context.UsersRooms.AddAsync(userRoom);
                await context.SaveChangesAsync();
                result.Data = userRoom.Id;
                result.IsSuccess = true;
                result.Message = ConstantMessageCodes.OPERATION_SUCCESS;
                result.ResponseCode = ResponseCodeEnum.SUCCESS;
                return result;
            }
            catch (Exception e)
            {
                logger.LogError($"error while requesting to join to room {roomId} with userId {userId} , error : {e}");
                result.IsSuccess = false;
                result.ResponseCode = ResponseCodeEnum.FAILED;
                result.Message = ConstantMessageCodes.OPERATION_FAILED;
                return result;
            }
        }

        public async Task<OperationResult<int>> ChangeUserRoomStatus(AppDbContext context, string userId, int roomId, string status, Dictionary<string, List<string>> allowedStatuses, string comment = "")
        {
            var room = await context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId && !x.IsDeleted);
            if (room == null)
            {
                return OperationResult.Fail<int>(ConstantMessageCodes.ROOM_NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            }
            //can't change the status of user room if the room is finished or stopped
            if (!IsValidRoomToJoin(room))
            {
                return OperationResult.Fail<int>(ConstantMessageCodes.ROOM_NOT_VAlID_TO_JOIN, default, ResponseCodeEnum.BAD_INPUT);
            }
            var userRoom = await context.UsersRooms.FirstOrDefaultAsync(x => x.RoomId == roomId && x.UserId == userId && !x.IsDeleted);
            if (userRoom == null)
            {
                if (!ConstantUserRoomStatus.IsPending(status))
                {
                    return OperationResult.Fail<int>(ConstantMessageCodes.CANT_CHANGE_STATUS, default, ResponseCodeEnum.BAD_INPUT);
                }
                userRoom = new UsersRooms
                {
                    UserId = userId,
                    RoomId = roomId,
                    IsDeleted = false,
                };
                //for requsting join to the room
                userRoom.StatusId = GetStatusForUserRoom(room,status);
                var userRoomHistory = new UserRoomsHistory
                {
                    Comment = userRoom.Comment,
                    LeaveReason = userRoom.LeaveReason,
                    RejectionReason = userRoom.RejectionReason,
                    StatusId = userRoom.StatusId,
                    UserId = userId,
                    UserRoomsId = userRoom.Id,
                    SuspensionReason = userRoom.SuspensionReason,
                    RoomId=roomId,
                };
                await context.UserRoomsHistories.AddAsync(userRoomHistory);
                await context.UsersRooms.AddAsync(userRoom);
            }
            else
            {
                var allowedStatusList = allowedStatuses.GetValueOrDefault(userRoom.StatusId);
                if (allowedStatusList == null)
                {
                    return OperationResult.Fail<int>(ConstantMessageCodes.INVALID_STATUS, default, ResponseCodeEnum.BAD_INPUT);
                }
                //if the status is not in the allowed status list it mean the user can't change to this status
                if (!allowedStatusList.Contains(status))
                {

                    return OperationResult.Fail<int>(ConstantMessageCodes.INVALID_STATUS, default, ResponseCodeEnum.BAD_INPUT);
                }
                userRoom.StatusId = status;
                var userRoomHistory = new UserRoomsHistory
                {
                    Comment = userRoom.Comment,
                    LeaveReason = userRoom.LeaveReason,
                    RejectionReason = userRoom.RejectionReason,
                    StatusId = userRoom.StatusId,
                    UserId = userId,
                    UserRoomsId = userRoom.Id,
                    SuspensionReason = userRoom.SuspensionReason,
                    RoomId=roomId,
                };
                await context.UserRoomsHistories.AddAsync(userRoomHistory);
                //accroding to the status , the reason will be updated , ( rejection reason for reject staus , etc.. )
                UpdateAppropiateComment(userRoom, status, comment);
            }
            await context.SaveChangesAsync();
            return OperationResult.Success(userRoom.Id);
        }

        public async Task<OperationResult<int>> AddMaterial(AppDbContext context, int roomId, IFormFile file)
        {
            var room = await context.Rooms.Select(x => new { x.Name, x.Id }).FirstOrDefaultAsync(x => x.Id == roomId);
            string parentFolder = "RoomMaterials";
            string roomName = room.Name;
            //add the file , take its name and its path and add it in the room material
            var addFileResult = await fileManager.Add(file, roomName, parentFolder);
            if (!addFileResult.IsSuccess)
            {
                return OperationResult.Fail<int>(addFileResult.Message, default, addFileResult.ResponseCode);
            }
            var material = new RoomMaterial
            {
                FileName = addFileResult.Data.FileName,
                IsActive = true,
                RoomId = roomId,
                FilePath = addFileResult.Data.FullPath,
            };
            await context.RoomMaterials.AddAsync(material);
            await context.SaveChangesAsync();
            return OperationResult.Success(material.Id);
        }
        public async Task<OperationResult<int>> UpdateNumberOfUsers(AppDbContext context, int roomId , int? requestedNumber = null,int? joinedNumber = null , int? leftNumber = null, int? rejectedNumber = null)
        {
            var room = await context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId && !x.IsDeleted);
            if(room == null)
            {
                return OperationResult.Fail<int>(ConstantMessageCodes.ROOM_NOT_FOUND,default, ResponseCodeEnum.NOT_FOUND);
            }
            if(requestedNumber != null)
                room.NumberOfRequestedUsers += requestedNumber.Value;
            if(joinedNumber != null)
                room.NumberOfJoinedUsers += joinedNumber.Value;
            if(leftNumber != null)
                room.NumberOfLeftUsers += leftNumber.Value;
            if(rejectedNumber != null)
                room.NumberOfRejectedUsers += rejectedNumber.Value;
            await context.SaveChangesAsync();
            return OperationResult.Success(room.NumberOfRequestedUsers);
        }
        public async Task<OperationResult<int>> UpdateNumberOfUsers(AppDbContext context, int roomId,string status, int number = 1)
        {
            var room = await context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId && !x.IsDeleted);
            if(room == null)
            {
                return OperationResult.Fail<int>(ConstantMessageCodes.ROOM_NOT_FOUND,default, ResponseCodeEnum.NOT_FOUND);
            }
            if(string.IsNullOrEmpty( status))
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND,default,ResponseCodeEnum.NOT_FOUND);
           if(status == ConstantUserRoomStatus.REJECTED)
            {
                room.NumberOfRejectedUsers += number;
            }
           else if(status == ConstantUserRoomStatus.JOINED)
            {
                room.NumberOfJoinedUsers+=number;
            }
            else if(status == ConstantUserRoomStatus.ACCEPTED)
            {
                room.NumberOfJoinedUsers+=number;
            }
            else if(status == ConstantUserRoomStatus.LEFT || status == ConstantUserRoomStatus.CANCELED)
            {
                room.NumberOfLeftUsers+=number;
            }
            else if(status == ConstantUserRoomStatus.PENDING)
            {
                room.NumberOfRequestedUsers+=number;
            }
            await context.SaveChangesAsync();
            return OperationResult.Success(room.NumberOfRequestedUsers);
        }

        
        #region Private Methods

        private bool IsValidRoomToJoin(Room room)
        {
            return room != null && ConstantRoomStatus.IsActiveStatus(room.StatusId);
        }

        private string GetStatusForUserRoom(Room room,string status = "")
        {
            if(string.IsNullOrEmpty(status) || status == ConstantUserRoomStatus.PENDING)
            {
                bool isRoomStarted = IsRoomStarted(room);
                if (isRoomStarted)
                {
                    return ConstantUserRoomStatus.JOINED;
                }
                if(room.IsPublic)
                {
                    return ConstantUserRoomStatus.ACCEPTED;
                }
                return ConstantUserRoomStatus.PENDING;
            }
            return status;
        }
        private string GetUserRoomStatusFromInvite(string inviteStatus,Room room)
        {
            if (inviteStatus == ConstantUserInvites.ACCEPTED)
            {
                return room.IsPublic ? ConstantUserRoomStatus.JOINED : ConstantUserRoomStatus.ACCEPTED;
            }
            return ConstantUserRoomStatus.CANCELED;
        }
        private bool IsRoomStarted(Room room)
        {
            return room.StartDate.Date >= DateTime.Now.Date;
        }

        private void UpdateAppropiateComment(UsersRooms usersRooms, string status, string comment)
        {
            switch (status)
            {
                case ConstantUserRoomStatus.REJECTED:
                    usersRooms.RejectionReason = comment;
                    break;

                case ConstantUserRoomStatus.SUSPENDED:
                    usersRooms.SuspensionReason = comment;
                    break;
                case ConstantUserRoomStatus.LEFT:
                    usersRooms.LeaveReason = comment;
                    break;
                default:
                    break;
            }
        }

        #endregion Private Methods
    }
}