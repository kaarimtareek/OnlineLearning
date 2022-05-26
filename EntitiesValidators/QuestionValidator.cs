using Microsoft.EntityFrameworkCore;

using OnlineLearning.Models;

using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public class QuestionValidator : IQuestionValidator
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public QuestionValidator(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }
        public async Task<bool> IsQuestionExist(int questionId, CancellationToken cancellationToken = default)
        {
            using AppDbContext context = new AppDbContext(dbContextOptions);
            return await context.Questions.AnyAsync(x => x.Id == questionId && !x.IsDeleted, cancellationToken);
        }
        public async Task<bool> IsAnswerExist(int answerId, CancellationToken cancellationToken = default)
        {
            using AppDbContext context = new AppDbContext(dbContextOptions);
            return await context.Answers.AnyAsync(x => x.Id == answerId && !x.IsDeleted, cancellationToken);
        }
    }
}
