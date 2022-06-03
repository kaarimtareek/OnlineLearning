using MediatR;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Services;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Commands
{
    public class AddUserInterestCommandHandler : IRequestHandler<AddUserInterestCommand, ResponseModel<int>>

    {
        private readonly IInterestService interestService;
        private readonly DbContextOptions<AppDbContext> contextOptions;

        public AddUserInterestCommandHandler(IInterestService interestService, DbContextOptions<AppDbContext> contextOptions)
        {
            this.interestService = interestService;
            this.contextOptions = contextOptions;
        }

        public async Task<ResponseModel<int>> Handle(AddUserInterestCommand request, CancellationToken cancellationToken)
        {

            using (var context = new AppDbContext(contextOptions))
            {
                //if we need to call more than one function , and one of them failed , use rollback to undo all the changes
                using var transactionScope = await context.Database.BeginTransactionAsync();
                {
                    try
                    {
                        OperationResult<int> result;
                        result = await interestService.AddUserInterest(context, request.UserId, request.InterestId);
                        if (!result.IsSuccess)
                        {
                            await transactionScope.RollbackAsync();
                        }
                        else
                        {
                            await interestService.UpdateInterestNumber(context, request.InterestId);
                            await transactionScope.CommitAsync();
                        }
                        return new ResponseModel<int>
                        {
                            IsSuccess = result.IsSuccess,
                            MessageCode = result.Message,
                            Result = result.Data,
                            HttpStatusCode = result.ResponseCode.GetStatusCode()
                        };
                    }
                    catch (Exception e)
                    {
                        await transactionScope.RollbackAsync();
                        return new ResponseModel<int>
                        {
                            IsSuccess = false,
                            HttpStatusCode = ResponseCodeEnum.FAILED.GetStatusCode()
                        };
                    }
                }
            }
        }
    }
}