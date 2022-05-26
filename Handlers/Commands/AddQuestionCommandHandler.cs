using MediatR;
using Microsoft.EntityFrameworkCore;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Commands
{
    public class AddQuestionCommandHandler : IRequestHandler<AddQuestionCommand, ResponseModel<int>>
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IQuestionService questionService;

        public AddQuestionCommandHandler(DbContextOptions<AppDbContext> contextOptions, IQuestionService questionService)
        {
            this.contextOptions=contextOptions;
            this.questionService=questionService;
        }

        public async Task<ResponseModel<int>> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                //if we need to call more than one function , and one of them failed , use rollback to undo all the changes
                using var transactionScope = await context.Database.BeginTransactionAsync();
                {
                    try
                    {
                        var result = await questionService.AddQuestion(context, request.RoomId, request.UserId, request.QuestionTitle, request.QuestionDescription);
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
