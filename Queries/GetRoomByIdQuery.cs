using MediatR;
using OnlineLearning.Common;
using OnlineLearning.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Queries
{
    public class GetRoomByIdQuery : IRequest<ResponseModel<Room>>
    {
        public int RoomId { get; set; }
    }
}
