using OnlineLearning.Common;
using OnlineLearning.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IRoomService
    {
        Task<OperationResult<int>> CreateRoom(AppDbContext context, string userId, string roomName, string roomDescription, decimal price, DateTime StartDate, DateTime? expectedEndDate, List<string> interests);
        Task<OperationResult<Room>> GetRoomById(int roomId);
    }
}