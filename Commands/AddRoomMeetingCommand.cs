﻿using MediatR;
using OnlineLearning.Common;

using System;

namespace OnlineLearning.Commands
{
    public class AddRoomMeetingCommand : IRequest<ResponseModel<int>>
    {
        public bool StartNow { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
        public int RoomId { get; set; }
        public int Duration { get; set; }
        public string UserId { get; set; }
        public string ZoomToken { get; set; }
    }
}