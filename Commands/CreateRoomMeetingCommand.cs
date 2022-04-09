using MediatR;
using OnlineLearning.Common;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Commands
{
    public class CreateRoomMeetingCommand : IRequest<ResponseModel<string>>
    {
        public string UserId { get; set; }
        [Required]
        [MinLength(5)]
        public string TopicName { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int RoomId { get; set; }
        public bool CreateNow { get; set; }
    }
}
