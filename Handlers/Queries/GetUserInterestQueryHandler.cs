using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using OnlineLearning.Services;

namespace OnlineLearning.Handlers.Queries
{
    public class GetUserInterestQueryHandler : IRequestHandler<GetUserInterestsQuery, ResponseModel<List<InterestDto>>>
    {
        private readonly IInterestService interestService;

        public GetUserInterestQueryHandler(IInterestService interestService)
        {
            this.interestService = interestService;
        }
        public async Task<ResponseModel<List<InterestDto>>> Handle(GetUserInterestsQuery request, CancellationToken cancellationToken)
        {
            var result = await interestService.GetUserIntersts(request.UserId);
            return new ResponseModel<List<InterestDto>>
            {
                HttpStatusCode = result.ResponseCode.GetStatusCode(),
                Result = result.Data.ConvertAll(x=> new InterestDto
                {
                    Id = x.Id,
                    IsDeleted = x.IsDeleted,
                }),
                IsSuccess = result.IsSuccess,
                MessageCode = result.Message,

            };
        }
    }
}
