using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public interface IRoomValidator
    {
        Task<bool> IsRoomStatusExist(string id);

        Task<bool> IsRoomStatusExist(string id, CancellationToken cancellationToken);

        Task<bool> IsRoomExist(int id);

        Task<bool> IsRoomExist(int id, CancellationToken cancellationToken);
        Task<bool> IsUserRoomOwner(int roomId, string userId);
        Task<bool> IsUserRoomOwner(int roomId, string userId, CancellationToken cancellationToken);
    }
}