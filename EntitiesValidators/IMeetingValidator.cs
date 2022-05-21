
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public interface IMeetingValidator
    {
        Task<bool> IsMeetingExist(int meetingId, CancellationToken cancellationToken = default);
    }
}