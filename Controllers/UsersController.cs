using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.InputModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("")]
        public async Task<IActionResult>Add(AddUserInputModel inputModel)
        {
            var command = new AddUserCommand
            {
                Name = inputModel.Name,
                BrithDate = inputModel.BrithDate,
                Email = inputModel.Email,
                Password = inputModel.Password,
                Phonenumber = inputModel.Phonenumber
            };
            var result = await mediator.Send(command);
            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
