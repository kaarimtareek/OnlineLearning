using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public interface IInterestValidator
    {
        bool IsAllInterestsExist(List<string> interestIds);
        //Task<bool> IsAllInterestsExist(List<string> interestIds, CancellationToken cancellationToken);
        Task<bool> IsInterestExist(string interestId);
        Task<bool> IsUserInterestExist(string userId, string interestId);
        Task<bool> IsUserInterestExist(string userId, string interestId, CancellationToken cancellationToken);
    }
}