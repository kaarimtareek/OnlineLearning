
using MediatR;

using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Queries;
using OnlineLearning.QueryParameters;

using System.Net;
using System;
using System.Threading.Tasks;
using OnlineLearning.Settings;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestsController : SystemBaseController
    {
        private readonly IMediator mediator;
        private readonly PaginationSettings paginationSettings;

        public InterestsController(IMediator mediator, PaginationSettings paginationSettings)
        {
            this.mediator = mediator;
            this.paginationSettings=paginationSettings;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetInterestsQueryParameters queryParameters)
        {
            try
            {
               queryParameters.HandleSettings(paginationSettings);
                var result = await mediator.Send(new GetInterestsQuery
                {
                    UserId = UserId,
                    PageSize = queryParameters.PageSize,
                    PageNumber = queryParameters.PageNumber,
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
