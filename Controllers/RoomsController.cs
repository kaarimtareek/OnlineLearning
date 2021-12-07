using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OnlineLearning.Commands;
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
    }
}
