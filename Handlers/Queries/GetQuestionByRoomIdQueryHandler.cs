using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Handlers.Queries
{
    public class GetQuestionByRoomIdQueryHandler : IRequestHandler<GetQuestionsByRoomIdQuery, ResponseModel<List<QuestionDto>>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetQuestionByRoomIdQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }

        public async Task<ResponseModel<List<QuestionDto>>> Handle(GetQuestionsByRoomIdQuery request, CancellationToken cancellationToken)
        {
            using(AppDbContext context = new AppDbContext(dbContextOptions))
            {

                var questions = await context.Questions.AsNoTracking().Include(x=>x.User).Where(x => x.RoomId == request.RoomId && !x.IsDeleted).Select(x => new QuestionDto
                {
                    RoomId = x.RoomId,
                    StatusId = x.StatusId,
                    QuestionDescription = x.Body,
                    QuestionTitle = x.Title,
                    UserId = x.UserId,
                    UserName = x.User.Name,
                    Id = x.Id,
                    IsAnswered = ConstantQuestionStatus.ANSWERED == x.StatusId
                }).ToListAsync();
                return ResponseModel.Success(ConstantMessageCodes.OPERATION_SUCCESS, questions, null, System.Net.HttpStatusCode.OK);

            }
        }
    }
}
