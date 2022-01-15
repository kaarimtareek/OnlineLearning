using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Commands;
using OnlineLearning.Constants;
using OnlineLearning.Models.InputModels;
using OnlineLearning.Queries;

using System;
using System.Collections.Generic;
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

        public RoomsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            try
            {
                var result =await mediator.Send(new GetRoomByIdQuery
                {
                    RoomId = id
                });
                return StatusCode((int)result.HttpStatusCode,result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("")]
        public async Task<IActionResult> GetRooms()
        {
            try
            {
                var result =await mediator.Send(new GetAvailableRoomsQuery
                {
                    UserId = UserId
                });
                return StatusCode((int)result.HttpStatusCode,result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateRoom(AddRoomInputModel inputModel)
        {
            try
            {
                var result =await mediator.Send(new AddRoomCommand
                {
                    RoomDescription = inputModel.RoomDescription,
                    ExpectedEndDate = inputModel.ExpectedEndDate,
                    Interests = inputModel.Interests,
                    Price = inputModel.Price,
                    RoomName = inputModel.RoomName,
                    StartDate = inputModel.StartDate,
                    UserId  = UserId
                });
                return StatusCode((int)result.HttpStatusCode,result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost("{roomId}/Join")]
        public async Task<IActionResult> RequestToJoinRoom(int roomId)
        {
            try
            {
                var result =await mediator.Send(new UserChangeUserRoomStatusCommand
                {
                    RoomId = roomId,
                    UserId  = UserId,
                    StatusId = ConstantRoomStatus.PENDING
                });
                return StatusCode((int)result.HttpStatusCode,result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("{roomId}/RequestedUsers")]
        public async Task<IActionResult> RequestedUsers(int roomId)
        {
            try
            {
                var result =await mediator.Send(new GetRequestedUsersToRoomQuery
                {
                    RoomId = roomId,
                    RoomOwnerId  = UserId
                });
                return StatusCode((int)result.HttpStatusCode,result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("{roomId}/AcceptUser/{userId}")]
        public async Task<IActionResult> AcceptUserToRoom(int roomId,string userId)
        {
            try
            {
                var result =await mediator.Send(new OwnerChangeUserRoomStatusCommand
                {
                    RoomId = roomId,
                    OwnerId  = UserId,
                    UserId = userId,
                    Reason ="",
                    StatusId = ConstantUserRoomStatus.ACCEPTED ,
                });
                return StatusCode((int)result.HttpStatusCode,result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpPut("{roomId}/RejectUser/{userId}/Reason/{comment}")]
        public async Task<IActionResult> RejectUserToRoom(int roomId,string userId,string comment = "")
        {
            try
            {
                var result =await mediator.Send(new OwnerChangeUserRoomStatusCommand
                {
                    RoomId = roomId,
                    OwnerId  = UserId,
                    UserId = userId,
                    Reason =comment,
                    StatusId = ConstantUserRoomStatus.REJECTED ,
                });
                return StatusCode((int)result.HttpStatusCode,result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
