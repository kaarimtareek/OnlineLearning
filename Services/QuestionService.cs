using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;

using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public class QuestionService : IQuestionService
    {
        public async Task<OperationResult<int>> AddQuestion(AppDbContext context, int roomId, string userId, string questionTitle, string questionDescription)
        {
            var question = new Question
            {
                RoomId = roomId,
                UserId = userId,
                Title = questionTitle,
                Body = questionTitle,
                StatusId = ConstantQuestionStatus.NOT_ANSWERED,
            };
            context.Questions.Add(question);
            await context.SaveChangesAsync();
            return OperationResult.Success(question.Id);
        }
        public async Task<OperationResult<int>> EditQuestion(AppDbContext context, int quesitonId, string questionTitle, string questionDescription)
        {
            var question = await context.Questions.SingleOrDefaultAsync(x => x.Id == quesitonId && !x.IsDeleted);
            if (question == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            question.Title = questionTitle;
            question.Body = questionDescription;
            await context.SaveChangesAsync();
            return OperationResult.Success(question.Id);
        }
        public async Task<OperationResult<int>> EditQuestion(AppDbContext context, Question question, string questionTitle, string questionDescription)
        {
            if (question == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            question.Title = questionTitle;
            question.Body = questionDescription;
            await context.SaveChangesAsync();
            return OperationResult.Success(question.Id);
        }
        public async Task<OperationResult<int>> ChangeQuestionStatus(AppDbContext context, int questionId,string statusId)
        {
            var question = await context.Questions.FirstOrDefaultAsync(x => x.Id == questionId && !x.IsDeleted);
            if (question == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            question.StatusId = statusId;
            await context.SaveChangesAsync();
            return OperationResult.Success(question.Id);
        }
        public async Task<OperationResult<int>> ChangeQuestionStatus(AppDbContext context, Question question,string statusId)
        {
            if (question == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            question.StatusId = statusId;
            await context.SaveChangesAsync();
            return OperationResult.Success(question.Id);
        }
        public async Task<OperationResult<int>> DeleteQuestion(AppDbContext context, Question question)
        {
            if (question == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            question.IsDeleted = true;
            question.StatusId = ConstantQuestionStatus.DELETED;
            await context.SaveChangesAsync();
            return OperationResult.Success(question.Id);
        }
        public async Task<OperationResult<int>> DeleteQuestion(AppDbContext context, int questionId)
        {
            var question = await context.Questions.FirstOrDefaultAsync(x => x.Id == questionId && !x.IsDeleted);
            if (question == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            question.IsDeleted = true;
            question.StatusId = ConstantQuestionStatus.DELETED;
            await context.SaveChangesAsync();
            return OperationResult.Success(question.Id);
        }
        public async Task<OperationResult<int>> DeleteAnswer(AppDbContext context, Answer answer)
        {
            if (answer == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            answer.IsDeleted = true;
            await context.SaveChangesAsync();
            return OperationResult.Success(answer.Id);
        }
        public async Task<OperationResult<int>> DeleteAnswer(AppDbContext context, int answerId)
        {
            var answer = await context.Answers.FirstOrDefaultAsync(x => x.Id == answerId && !x.IsDeleted);
            if (answer == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            answer.IsDeleted = true;
            await context.SaveChangesAsync();
            return OperationResult.Success(answer.Id);
        }
        public async Task<OperationResult<int>> AddAnswer(AppDbContext context, int questionId, string userId, string answerBody)
        {
            var answer = new Answer
            {
                QuestionId = questionId,
                Body = answerBody,
                UserId = userId,
            };
            context.Answers.Add(answer);
            await context.SaveChangesAsync();
            return OperationResult.Success(answer.Id);
        }
        public async Task<OperationResult<int>> AcceptAnswer(AppDbContext context, int answerId)
        {
            var answer = await context.Answers.FirstOrDefaultAsync(x => x.Id == answerId && !x.IsDeleted);
            if (answer == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            answer.IsAccepted = true;
            await context.SaveChangesAsync();
            return OperationResult.Success(answer.Id);
        }
        public async Task<OperationResult<int>> AcceptAnswer(AppDbContext context, Answer answer)
        {
            if (answer == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            answer.IsAccepted = true;
            await context.SaveChangesAsync();
            return OperationResult.Success(answer.Id);
        }
        public async Task<OperationResult<int>> EditAnswer(AppDbContext context, int answerId, string answerDescription)
        {
            var question = await context.Answers.SingleOrDefaultAsync(x => x.Id == answerId && !x.IsDeleted);
            if (question == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            question.Body = answerDescription;
            await context.SaveChangesAsync();
            return OperationResult.Success(question.Id);
        }
        public async Task<OperationResult<int>> EditAnswer(AppDbContext context, Answer answer, string questionDescription)
        {
            if (answer == null)
                return OperationResult.Fail<int>(ConstantMessageCodes.NOT_FOUND, default, ResponseCodeEnum.NOT_FOUND);
            answer.Body = questionDescription;
            await context.SaveChangesAsync();
            return OperationResult.Success(answer.Id);
        }
    }
}
