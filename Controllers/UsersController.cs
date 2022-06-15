using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Commands;
using OnlineLearning.Models.InputModels;
using OnlineLearning.Queries;

using System.Threading.Tasks;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : SystemBaseController
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var query = new GetUserByIdQuery
            {
                Id = UserId
            };
            var result = await mediator.Send(query);
            return StatusCode((int)result.HttpStatusCode, result);
        }

        [HttpPost("/Interests")]
        public async Task<IActionResult> AddInterest(AddUserInterestInputModel inputModel)
        {
            var query = new AddUserInterestCommand
            {
                UserId= UserId,
                Interests = inputModel.Interests,
            };
            var result = await mediator.Send(query);
            return StatusCode((int)result.HttpStatusCode, result);
        }
        [HttpGet("/Interests")]
        public async Task<IActionResult> GetInterests()
        {
            var query = new GetUserInterestsQuery
            {
                UserId= UserId,
            };
            var result = await mediator.Send(query);
            return StatusCode((int)result.HttpStatusCode, result);
        }
        [HttpDelete("/Interests/{interestId}")]
        public async Task<IActionResult> DeleteInterest(string interestId)
        {
            var command = new DeleteUserInterestCommand
            {
                UserId= UserId,
                InterestId= interestId
            };
            var result = await mediator.Send(command);
            return StatusCode((int)result.HttpStatusCode, result);
        }

    }
}
