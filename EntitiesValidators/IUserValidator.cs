using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public interface IUserValidator
    {
        Task<bool> IsAvailableEmail(string email);
        Task<bool> IsAvailableEmail(string email, CancellationToken cancellationToken);
        Task<bool> IsAvailablePhoneNumber(string phonenumber);
        Task<bool> IsAvailablePhoneNumber(string phonenumber, CancellationToken cancellationToken);
        Task<bool> IsExistingUserId(string id);
        Task<bool> IsExistingUserId(string id, CancellationToken cancellationToken);
        Task<bool> IsActiveUserId(string id);
        Task<bool> IsActiveUserId(string id, CancellationToken cancellationToken);
    }
}