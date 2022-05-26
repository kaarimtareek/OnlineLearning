using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public interface IQuestionValidator
    {
        Task<bool> IsAnswerExist(int answerId, CancellationToken cancellationToken = default);
        Task<bool> IsQuestionExist(int questionId, CancellationToken cancellationToken = default);
    }
}