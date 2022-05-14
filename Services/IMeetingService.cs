using OnlineLearning.Common;
using OnlineLearning.Models;

using System;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IMeetingService
    {
        Task<OperationResult<int>> AddMeeting(AppDbContext context, string userId, int roomId, string link, DateTime start, DateTime end, string topicName, string topiceDiscription, long zoomMeetingId, string meetingPassword, bool startNow);
        Task<OperationResult<int>> DeleteMeeting(AppDbContext context, int roomId, int meetingId);
        Task<OperationResult<int>> DeleteMeeting(AppDbContext context, RoomMeeting roomMeeting);
        Task<OperationResult<int>> UpdateMeeting(AppDbContext context, int roomId, int meetingId, string topicName, string topicDescription, bool startNow, DateTime? startTime, DateTime? endTime);
        Task<OperationResult<int>> UpdateMeeting(AppDbContext context, RoomMeeting roomMeeting, string topicName, string topicDescription, bool startNow, DateTime? startTime, DateTime? endTime);
    }
}