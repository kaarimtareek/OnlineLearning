using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.InputModels;
using OnlineLearning.Queries;

using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult>Get()
        {
            var query = new GetUserByIdQuery
            {
                Id = UserId
            };
            var result = await mediator.Send(query);
            return StatusCode((int)result.HttpStatusCode, result);
        }

    }
}
