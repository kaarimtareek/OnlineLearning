﻿using MediatR;

using OnlineLearning.Common;

using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Commands
{
    public class AddUserInterestCommand : IRequest<ResponseModel<int>>
    {
        public string UserId { get; set; }
        [Required]
        public string InterestId { get; set; }
    }
}
