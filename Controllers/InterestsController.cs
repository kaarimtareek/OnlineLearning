
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IMediator mediator;

        public InterestsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{

        //}
    }
}
