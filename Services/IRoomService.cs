using Microsoft.AspNetCore.Http;

using OnlineLearning.Common;
using OnlineLearning.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IRoomService
    {
        Task<OperationResult<int>> CreateRoom(AppDbContext context, string userId, string roomName, string roomDescription, decimal price, DateTime StartDate, DateTime? expectedEndDate, bool isPublic, List<string> interests);
        Task<OperationResult<Room>> GetRoomById(int roomId);
        Task<OperationResult<int>> RequestToJoinRoom(AppDbContext context, int roomId, string userId);
        Task<OperationResult<int>> AddMaterial(AppDbContext context, int roomId, IFormFile file);
        Task<OperationResult<int>> ChangeUserRoomStatus(AppDbContext context, string userId, int roomId, string status, Dictionary<string, List<string>> allowedStatuses, string comment = "");
        Task<OperationResult<int>> UpdateNumberOfUsers(AppDbContext context, int roomId, int? requestedNumber = null, int? joinedNumber = null, int? leftNumber = null, int? rejectedNumber = null);
        Task<OperationResult<int>> UpdateNumberOfUsers(AppDbContext context, int roomId, string status, int number = 1);
    }
}