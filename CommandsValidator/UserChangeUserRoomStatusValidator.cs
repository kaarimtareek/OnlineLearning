using FluentValidation;

using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class UserChangeUserRoomStatusValidator : AbstractValidator<UserChangeUserRoomStatusCommand>
    {
        private readonly IRoomValidator roomValidator;
        private readonly IUserValidator userValidator;

        public UserChangeUserRoomStatusValidator(IRoomValidator roomValidator, IUserValidator userValidator)
        {
            this.roomValidator = roomValidator;
            RuleFor(x => x.RoomId).NotEmpty()
                .MustAsync((id, canelationToken) => roomValidator.IsRoomExist(id, canelationToken));
            RuleFor(x => x.UserId).NotEmpty()
                .MustAsync((id, cancelationToken) => userValidator.IsActiveUserId(id, cancelationToken));
        }
    }
}
