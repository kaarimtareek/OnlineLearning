using MediatR;
using Microsoft.EntityFrameworkCore;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;
using OnlineLearning.Services;

using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Commands
{
    public class DeleteUserInterestCommandHandler : IRequestHandler<DeleteUserInterestCommand, ResponseModel<int>>
    {
        private readonly DbContextOptions<AppDbContext> contextOptions;
        private readonly IInterestService interestService;

        public DeleteUserInterestCommandHandler(DbContextOptions<AppDbContext> contextOptions,IInterestService interestService)
        {
            this.contextOptions=contextOptions;
            this.interestService=interestService;
        }

        public async Task<ResponseModel<int>> Handle(DeleteUserInterestCommand request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(contextOptions))
            {
                var result = await interestService.DeleteUserInterest(context, request.UserId, request.InterestId);
                return new ResponseModel<int>
                {
                    HttpStatusCode = result.ResponseCode.GetStatusCode(),
                    IsSuccess = result.IsSuccess,
                    MessageCode = result.Message,
                    Result = result.Data
                };
            }
        }
    }
}
