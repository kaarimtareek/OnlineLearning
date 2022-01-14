﻿using OnlineLearning.Common;
using OnlineLearning.Models;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IRoomService
    {
        Task<OperationResult<int>> CreateRoom(AppDbContext context, string userId, string roomName, string roomDescription, decimal price, DateTime StartDate, DateTime? expectedEndDate,bool isPublic, List<string> interests);
        Task<OperationResult<Room>> GetRoomById(int roomId);
        Task<OperationResult<int>> RequestToJoinRoom(AppDbContext context, int roomId, string userId);
    }
}