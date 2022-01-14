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

        public RoomService(DbContextOptions<AppDbContext> contextOptions, ILoggerService<RoomService> logger)
        {
            this.contextOptions = contextOptions;
            this.logger = logger;
        }
        public async Task<OperationResult<int>> CreateRoom(AppDbContext context, string userId, string roomName, string roomDescription, decimal price, DateTime StartDate, DateTime? expectedEndDate, bool isPublic, List<string> interests)
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
                return new OperationResult<int>
                {
                    IsSuccess = true,
                    Message = ConstantMessageCodes.OPERATION_SUCCESS,
                    ResponseCode = ResponseCodeEnum.SUCCESS,
                    Data = room.Id,
                };
            }
            catch (Exception e)
            {
                logger.LogError($"error in create room {e}");
                return new OperationResult<int>
                {
                    IsSuccess = false,
                    Message = ConstantMessageCodes.OPERATION_FAILED,
                    ResponseCode = ResponseCodeEnum.FAILED,
                };
            }
        }
        public async Task<OperationResult<Room>> GetRoomById( int roomId)
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
        public async Task<OperationResult<LookupRoomStatus>> GetLookupRoomStatusById( string id)
        {
            using var context = new AppDbContext(contextOptions);
            var status = await context.LookupRoomStatuses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if(status == null)
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
        public async Task<OperationResult<int>>RequestToJoinRoom(AppDbContext context,int roomId ,string userId)
        {
            OperationResult<int> result = new OperationResult<int>();
            try
            { 
                var room = await context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId && !x.IsDeleted);
                if(room == null)
                {
                    result.IsSuccess = false;
                    result.Message = ConstantMessageCodes.ROOM_NOT_FOUND;
                    result.ResponseCode = ResponseCodeEnum.NOT_FOUND;
                    return result;
                }
                //user can't join an owned room
                if(room.OwnerId == userId)
                {
                    result.IsSuccess = false;
                    result.Message = ConstantMessageCodes.CANT_JOIN_OWNED_ROOM;
                    result.ResponseCode = ResponseCodeEnum.BAD_INPUT;
                    return result;
                }
                if(!IsValidRoomToJoin(room))
                {
                    result.Message = ConstantMessageCodes.ROOM_NOT_VAlID_TO_JOIN;
                    result.ResponseCode = ResponseCodeEnum.INVALID_DATA;
                    return result;
                }
                UsersRooms userRoom = new UsersRooms
                {
                    RoomId = roomId,
                    UserId = userId,
                };
                userRoom.StatusId = GetStatusForUserRoom(room);
                await context.UsersRooms.AddAsync(userRoom);
                await context.SaveChangesAsync();
                result.Data = userRoom.Id;
                result.IsSuccess = true;
                result.Message = ConstantMessageCodes.OPERATION_SUCCESS;
                result.ResponseCode= ResponseCodeEnum.SUCCESS;
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
        #region Private Methods
        private bool IsValidRoomToJoin(Room room)
        {
            return room != null && ConstantRoomStatus.IsActiveStatus(room.StatusId);
        }
        private string GetStatusForUserRoom(Room room)
        {
            bool isRoomStarted = IsRoomStarted(room);
            if (room.IsPublic)
            {
                return isRoomStarted ? ConstantUserRoomStatus.JOINED : ConstantUserRoomStatus.ACCEPTED;
            }
            return ConstantUserRoomStatus.PENDING;
        }
        private bool IsRoomStarted(Room room)
        {
            return room.StartDate.Date >= DateTime.Now.Date;
        }
        #endregion
    }
}
