using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Handlers.Queries.Reports;
using OnlineLearning.Queries.Reports;
using OnlineLearning.QueryParameters;
using OnlineLearning.Settings;

using System.Threading.Tasks;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : SystemBaseController
    {
        private readonly IMediator mediator;
        private readonly PaginationSettings paginationSettings;

        public ReportsController(IMediator mediator, PaginationSettings paginationSettings)
        {
            this.mediator=mediator;
            this.paginationSettings=paginationSettings;
        }

        [HttpGet("Rooms/{roomId}")]
        public async Task<IActionResult> GetReportForRoom(int roomId)
        {
            var result = await mediator.Send(new GetActivityForRoomQuery
            {
                RoomId =roomId,
                UserId = UserId,
            });
            return StatusCode((int)result.HttpStatusCode, result);
        }
        [HttpGet("UserRooms")]
        public async Task<IActionResult> GetReportForUserRooms()
        {
            var result = await mediator.Send(new GetActivityForRequestedRoomsQuery
            {
                UserId = UserId,
            });
            return StatusCode((int)result.HttpStatusCode, result);
        }
        [HttpGet("UserCreatedRooms")]
        public async Task<IActionResult> GetReportForUserCreatedRooms([FromQuery] GetReportForUserCreatedRoomsQueryParameters queryParameters)
        {
            queryParameters.HandleSettings(paginationSettings);
            var result = await mediator.Send(new GetUserCreatedRoomsReportQuery
            {
                UserId = UserId,
                From = queryParameters.From,
                Statusess = queryParameters.Statusess,
                To = queryParameters.To,
            });
            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
