using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class JoinRoomValidator : AbstractValidator<JoinRoomCommand>
    {
        private readonly IRoomValidator roomValidator;
        private readonly IUserValidator userValidator;

        public JoinRoomValidator(IRoomValidator roomValidator,IUserValidator userValidator)
        {
            this.roomValidator = roomValidator;
            this.userValidator = userValidator;
            RuleFor(x => x.RoomId)
                .NotEmpty()
                .MustAsync((id, cancelationToken) => roomValidator.IsRoomExist(id, cancelationToken));
            RuleFor(x => x.UserId)
                .NotEmpty()
                .MustAsync((id, cancelationToken) => userValidator.IsActiveUserId(id, cancelationToken));
        }
    }
}
