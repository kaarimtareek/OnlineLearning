using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Commands;
using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models.InputModels;
using OnlineLearning.Queries;
using OnlineLearning.QueryParameters;
using OnlineLearning.Settings;

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineLearning.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RoomsController : SystemBaseController
    {
        private readonly IMediator mediator;
        private readonly PaginationSettings paginationSettings;

        public RoomsController(IMediator mediator, PaginationSettings paginationSettings)
        {
            this.mediator = mediator;
            this.paginationSettings = paginationSettings;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            try
            {
                var result = await mediator.Send(new GetRoomByIdQuery
                {
                    RoomId = id
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("")]
        public async Task<IActionResult> GetRooms()
        {
            try
            {
                var result = await mediator.Send(new GetAvailableRoomsQuery
                {
                    UserId = UserId
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("CreatedRooms")]
        public async Task<IActionResult> GetCreatedRooms()
        {
            try
            {
                var result = await mediator.Send(new GetCreatedRoomsQuery
                {
                    UserId = UserId
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateRoom(AddRoomInputModel inputModel)
        {
            try
            {
                var result = await mediator.Send(new AddRoomCommand
                {
                    RoomDescription = inputModel.RoomDescription,
                    ExpectedEndDate = inputModel.ExpectedEndDate,
                    Interests = inputModel.Interests,
                    Price = inputModel.Price,
                    RoomName = inputModel.RoomName,
                    StartDate = inputModel.StartDate,
                    UserId = UserId
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost("{roomId}/Join")]
        public async Task<IActionResult> RequestToJoinRoom(int roomId)
        {
            try
            {
                var result = await mediator.Send(new UserChangeUserRoomStatusCommand
                {
                    RoomId = roomId,
                    UserId = UserId,
                    StatusId = ConstantRoomStatus.PENDING
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("{roomId}/RequestedUsers")]
        public async Task<IActionResult> RequestedUsers(int roomId)
        {
            try
            {
                var result = await mediator.Send(new GetRequestedUsersToRoomQuery
                {
                    RoomId = roomId,
                    RoomOwnerId = UserId
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("/Interests/GetRooms/{interestId}")]
        public async Task<IActionResult> GetRoomsByInterestId(string interestId, [FromQuery] GetRoomsByIntersetIdQueryParameters queryParameters)
        {
            try
            {
               queryParameters.HandleSettings(paginationSettings);
                var result = await mediator.Send(new GetRoomsByInterestIdQuery
                {
                    InterestId = interestId,
                    PageNumber = queryParameters.PageNumber,
                    PageSize = queryParameters.PageSize,
                    UserId = UserId

                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("/Interests/GetRooms")]
        public async Task<IActionResult> GetRoomsByInterests([FromQuery] GetRoomsByIntersetsQueryParameters queryParameters)
        {
            try
            {
                queryParameters.HandleSettings(paginationSettings);
                
                var result = await mediator.Send(new GetRoomsByInterestsQuery
                {
                    Interests = queryParameters.Interests,
                    PageNumber = queryParameters.PageNumber,
                    PageSize = queryParameters.PageSize,
                    UserId = UserId

                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("{roomId}/AcceptUser/{userId}")]
        public async Task<IActionResult> AcceptUserToRoom(int roomId, string userId)
        {
            try
            {
                var result = await mediator.Send(new OwnerChangeUserRoomStatusCommand
                {
                    RoomId = roomId,
                    OwnerId = UserId,
                    UserId = userId,
                    Reason = "",
                    StatusId = ConstantUserRoomStatus.ACCEPTED,
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("{roomId}/RejectUser/{userId}")]
        public async Task<IActionResult> RejectUserToRoom(int roomId, string userId, [FromBody] RejectUserInRoomInputModel inputModel)
        {
            try
            {
                var result = await mediator.Send(new OwnerChangeUserRoomStatusCommand
                {
                    RoomId = roomId,
                    OwnerId  = UserId,
                    UserId = userId,
                    Reason =inputModel.Reason,
                    StatusId = ConstantUserRoomStatus.REJECTED,
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("{roomId}/users/status/{statusId}")]
        public async Task<IActionResult> RejectUserToRoom(int roomId, string statusId)
        {
            try
            {
                var result = await mediator.Send(new UserChangeUserRoomStatusCommand
                {
                    RoomId = roomId,
                    UserId = UserId,
                    StatusId = statusId,
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost("{roomId}/Materials")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddRoomMaterial(int roomId)
        {
            try
            {
                var file = HttpContext.Request?.Form?.Files.FirstOrDefault();
                if (file ==null)
                {
                    var badResponse = ResponseModel.Fail<int>(ConstantMessageCodes.FILE_NOT_FOUND);
                    return StatusCode((int)HttpStatusCode.BadRequest, badResponse);
                }
                var result = await mediator.Send(new AddRoomMaterialCommand
                {
                    RoomId = roomId,
                    UserId = UserId,
                    File = file,
                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("{roomId}/Materials")]
        public async Task<IActionResult> GetRoomMaterials(int roomId)
        {
            try
            {

                var result = await mediator.Send(new GetRoomMaterialsQuery
                {
                    RoomId = roomId,
                    UserId = UserId,

                });
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("{roomId}/Materials/{id}")]
        public async Task<IActionResult> GetRoomMaterialById(int roomId, int id)
        {
            try
            {

                var result = await mediator.Send(new GetRoomMaterialQuery
                {
                    RoomId = roomId,
                    UserId = UserId,
                    MaterialId = id,


                });
                if(result.IsSuccess)
                {
                    return File(result.Result.Content, result.Result.ContentType);
                }
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("{roomId}/Meetings")]
        public async Task<IActionResult> CreateMeeting(int roomId, [FromBody] AddRoomMeetingInputModel inputModel)
        {
            var result = await mediator.Send(new AddRoomMeetingCommand
            {
                RoomId = roomId,
                UserId = UserId,
                StartNow =  inputModel.StartNow,
                EndTime = inputModel.EndTime,
                StartTime = inputModel.StartTime,
                TopicName = inputModel.TopicName,
                TopicDescription = inputModel.TopicDescription,
                ZoomToken = inputModel.ZoomToken,

            });
            return StatusCode((int)result.HttpStatusCode, result);
        }
        [HttpDelete("{roomId}/Meetings/{meetingId}")]
        public async Task<IActionResult> DeleteMeeting(int roomId, int meetingId, [FromBody] DeleteRoomMeetingInputModel inputModel)
        {
            var result = await mediator.Send(new DeleteRoomMeetingCommand
            {
                RoomId = roomId,
                UserId = UserId,
                MeetingId = meetingId,
                ZoomToken = inputModel.ZoomToken,

            });
            return StatusCode((int)result.HttpStatusCode, result);
        }
        [HttpPut("{roomId}/Meetings/{meetingId}")]
        public async Task<IActionResult> UpdateMeeting(int roomId, int meetingId, [FromBody] UpdateRoomMeetingInputModel inputModel)
        {
            var result = await mediator.Send(new UpdateRoomMeetingCommand
            {
                RoomId = roomId,
                UserId = UserId,
                MeetingId = meetingId,
                ZoomToken = inputModel.ZoomToken,
                Duration = inputModel.Duration,
                EndDate = inputModel.EndTime,
                MeetingDescription = inputModel.TopicDescription,
                MeetingName = inputModel.TopicName,
                StartDate = inputModel.StartTime,
                StartNow = inputModel.StartNow,

            });
            return StatusCode((int)result.HttpStatusCode, result);
        }
        [HttpGet("/Meetings/{meetingId}")]
        public async Task<IActionResult> GetMeeting(int meetingId)
        {
            var result = await mediator.Send(new GetRoomMeetingByIdQuery
            {
                RoomMeetingId = meetingId,
                UserId = UserId,


            });
            return StatusCode((int)result.HttpStatusCode, result);
        }
    }
}
