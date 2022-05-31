using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Services;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace OnlineLearning.Handlers.Commands
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, ResponseModel<int>>
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IQuestionService questionService;

        public DeleteQuestionCommandHandler(DbContextOptions<AppDbContext> contextOptions, IQuestionService questionService)
        {
            this.contextOptions=contextOptions;
            this.questionService=questionService;
        }

        public async Task<ResponseModel<int>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                //if we need to call more than one function , and one of them failed , use rollback to undo all the changes
                using var transactionScope = await context.Database.BeginTransactionAsync();
                {
                    try
                    {
                        var question = await context.Questions.Include(x => x.Answers).FirstOrDefaultAsync(x => x.Id == request.QuestionId && !x.IsDeleted);
                        var result = await questionService.DeleteQuestion(context, question);
                        if (!result.IsSuccess)
                        {
                            await transactionScope.RollbackAsync(cancellationToken);
                            return ResponseModel.Fail<int>(result.Message, default, null, result.ResponseCode.GetStatusCode());
                        }

                        foreach (var answer in question.Answers)
                        {
                            var answerResult = await questionService.DeleteAnswer(context, answer);
                        }
                        await transactionScope.CommitAsync(cancellationToken);
                        return ResponseModel.Success(result.Message, result.Data, null, result.ResponseCode.GetStatusCode());
                    }
                    catch (Exception ex)
                    {
                        await transactionScope.RollbackAsync(cancellationToken);
                        return ResponseModel.Fail<int>();
                    }
                }
            }
        }
    }
}
