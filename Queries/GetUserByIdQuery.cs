using MediatR;

using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Queries
{
    public class GetUserByIdQuery : IRequest<ResponseModel<UserDto>>
    {
        public string Id { get; set; }
    }
}
