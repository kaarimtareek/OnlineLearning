using MediatR;
using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetInterestsQueryHandler : IRequestHandler<GetInterestsQuery, ResponseModel<PagedList<InterestDto>>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetInterestsQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }

        public async Task<ResponseModel<PagedList<InterestDto>>> Handle(GetInterestsQuery request, CancellationToken cancellationToken)
        {
            using (var context = new AppDbContext(dbContextOptions))
            {
                var interests = context.Interests.Where(x => !x.IsDeleted).OrderBy(x => x.CreatedAt).Select(x => new InterestDto { Id = x.Id, IsDeleted = x.IsDeleted });
                var pagedInterests = await PagedList<InterestDto>.ToPagedList(interests, request.PageNumber, request.PageSize);
                return new ResponseModel<PagedList<InterestDto>>
                {
                    HttpStatusCode = ResponseCodeEnum.SUCCESS.GetStatusCode(),
                    IsSuccess  = true,
                    MessageCode = ConstantMessageCodes.OPERATION_SUCCESS,
                    Result = pagedInterests
                };
            }
        }
    }
}
