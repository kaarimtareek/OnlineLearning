using MediatR;
using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.Services;

using System.Collections.Generic;

namespace OnlineLearning.Queries
{
    public class GetUserInvitesQuery : IRequest<ResponseModel<List<InviteDto>>>
    {
        public string UserId { get; set; }
    }
}
