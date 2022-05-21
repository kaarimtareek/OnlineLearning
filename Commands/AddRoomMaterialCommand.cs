﻿
using MediatR;

using Microsoft.AspNetCore.Http;

using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class AddRoomMaterialCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public int RoomId { get; set; }
        public IFormFile File { get; set; }
    }
}
