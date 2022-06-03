using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Queries.Reports;

using System.Threading.Tasks;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : SystemBaseController
    {
        private readonly IMediator mediator;

        public ReportsController(IMediator mediator)
        {
            this.mediator=mediator;
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
    }
}
