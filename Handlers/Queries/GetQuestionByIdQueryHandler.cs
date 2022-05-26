using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;
using OnlineLearning.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Net;

namespace OnlineLearning.Handlers.Queries
{
    public class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, ResponseModel<QuestionDto>>
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public GetQuestionByIdQueryHandler(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }

        public async Task<ResponseModel<QuestionDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            using (AppDbContext context = new AppDbContext(dbContextOptions))
            {

                var questions = await context.Questions.AsNoTracking().Include(x => x.User).Include(x=>x.Answers).ThenInclude(x=>x.User).Where(x => x.Id == request.QuestionId && !x.IsDeleted).Select(x => new QuestionDto
                {
                    RoomId = x.RoomId,
                    StatusId = x.StatusId,
                    QuestionDescription = x.Body,
                    QuestionTitle = x.Title,
                    UserId = x.UserId,
                    UserName = x.User.Name,
                    Answers = x.Answers.Select(a=>
                    new AnswerDto
                    {
                        AnswerId = a.Id,
                        AnswerDescription = a.Body,
                        IsAccepted = a.IsAccepted,
                        QuestionId = a.QuestionId,
                        UserId = a.UserId,
                        UserName = a.User.Name
                    }).ToList()
                }).FirstOrDefaultAsync();
                if (questions == null)
                    return ResponseModel.Fail<QuestionDto>(ConstantMessageCodes.NOT_FOUND, default, null, HttpStatusCode.NotFound);
                return ResponseModel.Success(ConstantMessageCodes.OPERATION_SUCCESS, questions, null, System.Net.HttpStatusCode.OK);

            }
        }
    }
}
