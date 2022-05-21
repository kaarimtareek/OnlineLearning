using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;

namespace OnlineLearning.Queries
{
    public class GetUserByIdQuery : IRequest<ResponseModel<UserDto>>
    {
        public string Id { get; set; }
    }
}
