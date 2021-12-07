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
        public async Task<OperationResult<int>> CreateRoom(AppDbContext context, string userId, string roomName, string roomDescription, decimal price, DateTime StartDate, DateTime? expectedEndDate, List<string> interests)
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
                    
                };
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
    }
}
