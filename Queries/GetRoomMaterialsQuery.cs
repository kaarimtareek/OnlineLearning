using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.Models.OutputModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Queries
{
    public class GetRoomMaterialsQuery : IRequest<ResponseModel<List<RoomMaterialDto>>>
    {
        public int RoomId { get; set; }
        public string UserId { get; set; }
    }
}
