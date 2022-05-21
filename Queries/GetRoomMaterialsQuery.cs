using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;

using System.Collections.Generic;

namespace OnlineLearning.Queries
{
    public class GetRoomMaterialsQuery : IRequest<ResponseModel<List<RoomMaterialDto>>>
    {
        public int RoomId { get; set; }
        public string UserId { get; set; }
    }
}
