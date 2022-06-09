using MediatR;

using OnlineLearning.Common;

using System;
using System.Collections.Generic;

namespace OnlineLearning.Commands
{
    public class AddRoomCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        public string RoomName { get; set; }
        public string RoomDescription { get; set; }
        public decimal Price { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsPublic { get; set; }
        public bool StartNow { get; set; }
        public List<string> Interests { get; set; }
    }
}
