using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Queries
{
    public class GetRoomByIdQuery : IRequest<ResponseModel<RoomDto>>
    {
        public int RoomId { get; set; }
    }
}
