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
    public class AcceptAnswerCommandHandler : IRequestHandler<AcceptAnswerCommand, ResponseModel<int>>
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IQuestionService questionService;

        public AcceptAnswerCommandHandler(DbContextOptions<AppDbContext> contextOptions, IQuestionService questionService)
        {
            this.contextOptions=contextOptions;
            this.questionService=questionService;
        }

        public async Task<ResponseModel<int>> Handle(AcceptAnswerCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                //if we need to call more than one function , and one of them failed , use rollback to undo all the changes
                using var transactionScope = await context.Database.BeginTransactionAsync();
                {
                    try
                    {
                        var answer = await context.Answers.FirstOrDefaultAsync(x => x.Id == request.AnswerId && !x.IsDeleted);
                        var result = await questionService.AcceptAnswer(context, answer);
                        if (!result.IsSuccess)
                        {
                            await transactionScope.RollbackAsync(cancellationToken);
                            return ResponseModel.Fail<int>(result.Message, default, null, result.ResponseCode.GetStatusCode());
                        }

                        var questionResult = await questionService.ChangeQuestionStatus(context, answer.QuestionId, ConstantQuestionStatus.ANSWERED);
                        if (!questionResult.IsSuccess)
                        {
                            await transactionScope.RollbackAsync(cancellationToken);
                            return ResponseModel.Fail<int>(questionResult.Message, default, null, questionResult.ResponseCode.GetStatusCode());
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
