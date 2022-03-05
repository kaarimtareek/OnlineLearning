using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Queries
{
    public class GetRoomMaterialQuery : IRequest<ResponseModel<RoomMaterialDto>>
    {
        public int RoomId { get; set; }
        public int MaterialId { get; set; }
        public string UserId { get; set; }
    }
}
