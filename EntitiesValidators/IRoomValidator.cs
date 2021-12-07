using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public interface IRoomValidator
    {
        Task<bool> IsRoomStatusExist(string id);
        Task<bool> IsRoomStatusExist(string id, CancellationToken cancellationToken);
    }
}