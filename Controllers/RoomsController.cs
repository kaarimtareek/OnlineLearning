﻿using MediatR;

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
                int defaultPageNumber = paginationSettings.DefaultPageNumber;
                int maxPageSize = paginationSettings.MaxPageSize;
                int defaultPageSize = paginationSettings.DefaultPageSize;
                if (queryParameters.PageNumber < 1)
                    queryParameters.PageNumber = defaultPageNumber;
                if (queryParameters.PageSize < 1)
                    queryParameters.PageSize = defaultPageSize;
                if (queryParameters.PageSize > maxPageSize)
                    queryParameters.PageSize = maxPageSize;
                var result = await mediator.Send(new GetRoomsByInterestIdQuery
                {
                    InterestId = interestId,
                    PageNumber = queryParameters.PageNumber,
                    PageSize = queryParameters.PageSize,

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
                int defaultPageNumber = paginationSettings.DefaultPageNumber;
                int maxPageSize = paginationSettings.MaxPageSize;
                int defaultPageSize = paginationSettings.DefaultPageSize;
                if (queryParameters.PageNumber < 1)
                    queryParameters.PageNumber = defaultPageNumber;
                if (queryParameters.PageSize < 1)
                    queryParameters.PageSize = defaultPageSize;
                if (queryParameters.PageSize > maxPageSize)
                    queryParameters.PageSize = maxPageSize;
                var result = await mediator.Send(new GetRoomsByInterestsQuery
                {
                    Interests = queryParameters.Interests,
                    PageNumber = queryParameters.PageNumber,
                    PageSize = queryParameters.PageSize,

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
        [HttpPut("{roomId}/RejectUser/{userId}/Reason/{comment}")]
        public async Task<IActionResult> RejectUserToRoom(int roomId, string userId, [FromBody] RejectUserInRoomInputModel inputModel)
        {
            try
            {
                var result =await mediator.Send(new OwnerChangeUserRoomStatusCommand
                {
                    RoomId = roomId,
                    OwnerId  = UserId,
                    UserId = userId,
                    Reason =inputModel.Comment,
                    StatusId = ConstantUserRoomStatus.REJECTED ,
                });
                return StatusCode((int)result.HttpStatusCode,result);
            }
            catch(Exception e)
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
                if(file ==null)
                {
                    var badResponse = ResponseModel.Fail<int>(ConstantMessageCodes.FILE_NOT_FOUND);
                    return StatusCode((int)HttpStatusCode.BadRequest, badResponse);
                }
                var result =await mediator.Send(new AddRoomMaterialCommand
                {
                    RoomId = roomId,
                    UserId = UserId,
                    File = file,
                });
                return StatusCode((int)result.HttpStatusCode,result);
            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet("{roomId}/Materials")]
        public async Task<IActionResult> GetRoomMaterials(int roomId)
        {
            try
            {
                
                var result =await mediator.Send(new GetRoomMaterialsQuery
                {
                    RoomId = roomId,
                    UserId = UserId,
                 
                });
                return StatusCode((int)result.HttpStatusCode,result);
            }
            catch(Exception e)
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
                return StatusCode((int)result.HttpStatusCode, result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
