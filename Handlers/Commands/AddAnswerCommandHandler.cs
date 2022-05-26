using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Models;
using OnlineLearning.Services;
using System.Threading.Tasks;
using System.Threading;
using System;
using OnlineLearning.Constants;

namespace OnlineLearning.Handlers.Commands
{
    public class AddAnswerCommandHandler : IRequestHandler<AddAnswerCommand, ResponseModel<int>>
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IQuestionService questionService;

        public AddAnswerCommandHandler(DbContextOptions<AppDbContext> contextOptions, IQuestionService questionService)
        {
            this.contextOptions=contextOptions;
            this.questionService=questionService;
        }

        public async Task<ResponseModel<int>> Handle(AddAnswerCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                //if we need to call more than one function , and one of them failed , use rollback to undo all the changes
                using var transactionScope = await context.Database.BeginTransactionAsync();
                {
                    try
                    {
                        var result = await questionService.AddAnswer(context, request.QuestionId, request.UserId, request.AnswerDescription);
                        if (!result.IsSuccess)
                        {
                            await transactionScope.RollbackAsync();
                            return ResponseModel.Fail<int>(result.Message, default, null, result.ResponseCode.GetStatusCode());
                        }
                        await transactionScope.CommitAsync();
                        return ResponseModel.Success<int>(result.Message, result.Data, null, result.ResponseCode.GetStatusCode());
                    }
                    catch (Exception ex)
                    {
                        await transactionScope.RollbackAsync();
                        return ResponseModel.Fail<int>();
                    }
                }
            }
        }
    }
}
