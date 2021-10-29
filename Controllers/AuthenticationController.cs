using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Commands;

using OnlineLearning.InputModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthenticationController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Add(AddUserInputModel inputModel)
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
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserInputModel inputModel)
        {
            var command = new LoginUserCommand
            {
                Password = inputModel.Password,
                Username = inputModel.Username
            };
            var result = await mediator.Send(command);
            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
