using OnlineLearning.Common;
using OnlineLearning.Models;

using System;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IMeetingService
    {
        Task<OperationResult<int>> AddMeeting(AppDbContext context, string userId, int roomId, string link, DateTime start, DateTime end, string topicName, string topiceDiscription, int zoomMeetingId, string meetingPassword, bool startNow);
    }
}