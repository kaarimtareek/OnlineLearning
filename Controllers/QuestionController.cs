using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Commands;
using OnlineLearning.Models.InputModels;
using OnlineLearning.Queries;
using OnlineLearning.Settings;

using System;
using System.Net;
using System.Threading.Tasks;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : SystemBaseController
    {
        private readonly IMediator mediator;
        private readonly PaginationSettings paginationSettings;

        public QuestionController(PaginationSettings paginationSettings, IMediator mediator)
        {
            this.paginationSettings=paginationSettings;
            this.mediator=mediator;
        }
        [HttpPost("{roomId}/Questions")]
        public async Task<IActionResult> AddQuestion(int roomId, [FromBody] AddQuestionInputModel inputModel)
        {
            try
            {
                var result = await mediator.Send(new AddQuestionCommand
                {
                    QuestionDescription = inputModel.QuestionDescription,
                    QuestionTitle = inputModel.QuestionTitle,
                    RoomId = roomId,
                    UserId = UserId
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost("{roomId}/Questions/{questionId}/Answers")]
        public async Task<IActionResult> AddAnswer(int roomId,int questionId, [FromBody] AddAnswerInputModel inputModel)
        {
            try
            {
                var result = await mediator.Send(new AddAnswerCommand
                {
                   
                    AnswerDescription  = inputModel.AnswerDescription,
                    RoomId = roomId,
                    UserId = UserId,
                    QuestionId = questionId,
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("{roomId}/Questions")]
        public async Task<IActionResult> GetQuestions(int roomId)
        {
            try
            {
                var result = await mediator.Send(new GetQuestionsByRoomIdQuery
                {
                   
                    RoomId = roomId,
                    UserId = UserId,
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("{questionId}")]
        public async Task<IActionResult> GetQuestion(int questionId)
        {
            try
            {
                var result = await mediator.Send(new GetQuestionByIdQuery
                {
                   
                    UserId = UserId,
                    QuestionId =questionId,
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            try
            {
                var result = await mediator.Send(new DeleteQuestionCommand
                {
                   
                    UserId = UserId,
                    QuestionId =questionId,
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("{questionId}")]
        public async Task<IActionResult> EditQuestion(int questionId, [FromBody] EditQuestionInputModel inputModel)
        {
            try
            {
                var result = await mediator.Send(new EditQuestionCommand
                {
                   
                    UserId = UserId,
                    QuestionId =questionId,
                    QuestionDescription = inputModel.QuestionDescription,
                    QuestionTitle = inputModel.QuestionTitle,
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpDelete("Answers/{answerId}")]
        public async Task<IActionResult> DeleteAnswer(int answerId)
        {
            try
            {
                var result = await mediator.Send(new DeleteAnswerCommand
                {
                   
                    UserId = UserId,
                    AnswerId = answerId
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("Answers/{answerId}/Accept")]
        public async Task<IActionResult> AcceptAnswer(int answerId)
        {
            try
            {
                var result = await mediator.Send(new AcceptAnswerCommand
                {
                    UserId = UserId,
                    AnswerId = answerId
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("Answers/{answerId}")]
        public async Task<IActionResult> EditAnswer(int answerId, [FromBody] EditAnswerInputModel inputModel)
        {
            try
            {
                var result = await mediator.Send(new EditAnswerCommand
                {
                    UserId = UserId,
                    AnswerId = answerId,
                    AnswerDescription = inputModel.AnswerDescription
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
