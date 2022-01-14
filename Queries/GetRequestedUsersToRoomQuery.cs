using System.Collections.Generic;

using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;

namespace OnlineLearning.Queries
{
    public class GetRequestedUsersToRoomQuery : IRequest<ResponseModel<List<UserDto>>>
    {
        public string RoomOwnerId { get; set; }
        public int RoomId { get; set; }
    }
}
