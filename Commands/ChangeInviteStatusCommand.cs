using MediatR;
using OnlineLearning.Common;

namespace OnlineLearning.Commands
{
    public class ChangeInviteStatusCommand : IRequest<ResponseModel<int>>
    {
        public int InviteId { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
    }
}
