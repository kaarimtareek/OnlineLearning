using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.CommandsValidator
{
    public class CreateRoomMeetingValidator : AbstractValidator<CreateRoomMeetingCommand>
    {

        public CreateRoomMeetingValidator(IRoomValidator roomValidator, IUserValidator userValidator)
        {

            RuleFor(x => x.UserId)
                .MustAsync((m, x,c) => userValidator.IsActiveUserId(x,c))
                .WithMessage("User Not found")
                .MustAsync((m, x, c) => roomValidator.IsUserRoomOwner(m.RoomId, x, c))
                .WithMessage("User it not the room owner");
        }
    }
}
