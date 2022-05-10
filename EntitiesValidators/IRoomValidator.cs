using OnlineLearning.Models;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public interface IRoomValidator
    {

        Task<bool> IsRoomStatusExist(string id, CancellationToken cancellationToken);

        Task<bool> IsActiveRoom(int roomId, CancellationToken cancellationToken = default);
        Task<bool> IsRoomExist(int id, CancellationToken cancellationToken = default);
        Task<bool> IsUserRoomOwner(int roomId, string userId, CancellationToken cancellationToken = default);
        Task<bool> IsUserCanCreateMeeting(string userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        string GetOverlappedMeetings(string userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
    }
}