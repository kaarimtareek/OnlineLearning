using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public interface IQuestionValidator
    {
        Task<bool> IsAnswerExist(int answerId, CancellationToken cancellationToken = default);
        Task<bool> IsQuestionExist(int questionId, CancellationToken cancellationToken = default);
        Task<bool> IsQuestionOwnerForAnswer(int answerId, string userId, CancellationToken cancellationToken = default);
        Task<bool> IsAnswerOwner(int answerId, string userId, CancellationToken cancellationToken = default);
        Task<bool> IsQuestionOwner(int questionId, string userId, CancellationToken cancellationToken = default);
    }
}