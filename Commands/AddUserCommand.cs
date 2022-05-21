﻿using MediatR;

using OnlineLearning.Common;

using System;

namespace OnlineLearning.Commands
{
    public class AddUserCommand : IRequest<ResponseModel<string>>
    {
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime? BrithDate { get; set; }
    }
}
