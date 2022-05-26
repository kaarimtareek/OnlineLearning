using OnlineLearning.Common;
using OnlineLearning.Models;

using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IQuestionService
    {
        Task<OperationResult<int>> AcceptAnswer(AppDbContext context, Answer answer);
        Task<OperationResult<int>> AcceptAnswer(AppDbContext context, int answerId);
        Task<OperationResult<int>> AddAnswer(AppDbContext context, int questionId, string userId, string answerBody);
        Task<OperationResult<int>> AddQuestion(AppDbContext context, int roomId, string userId, string questionTitle, string questionDescription);
        Task<OperationResult<int>> DeleteAnswer(AppDbContext context, Answer answer);
        Task<OperationResult<int>> DeleteAnswer(AppDbContext context, int answerId);
        Task<OperationResult<int>> DeleteQuestion(AppDbContext context, int questionId);
        Task<OperationResult<int>> DeleteQuestion(AppDbContext context, Question question);
        Task<OperationResult<int>> EditAnswer(AppDbContext context, Answer answer, string questionDescription);
        Task<OperationResult<int>> EditAnswer(AppDbContext context, int answerId, string answerDescription);
        Task<OperationResult<int>> EditQuestion(AppDbContext context, int quesitonId, string questionTitle, string questionDescription);
        Task<OperationResult<int>> EditQuestion(AppDbContext context, Question question, string questionTitle, string questionDescription);
    }
}