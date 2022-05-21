using FluentValidation;

using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class OwnerChangeUserRoomStatusValidator : AbstractValidator<OwnerChangeUserRoomStatusCommand>
    {
        private readonly IRoomValidator roomValidator;
        private readonly IUserValidator userValidator;

        public OwnerChangeUserRoomStatusValidator(IRoomValidator roomValidator, IUserValidator userValidator)
        {
            this.roomValidator = roomValidator;
            RuleFor(x => x.RoomId).NotEmpty()
                .MustAsync((id, canelationToken) => roomValidator.IsRoomExist(id, canelationToken));
            RuleFor(x => x.UserId).NotEmpty()
                .MustAsync((id, cancelationToken) => userValidator.IsActiveUserId(id, cancelationToken));
            RuleFor(x => x.OwnerId).NotEmpty()
                .MustAsync((model, id, cancelationToken) => roomValidator.IsUserRoomOwner(model.RoomId, id, cancelationToken))
                .WithMessage("Not the room owner");
            this.userValidator = userValidator;
        }
    }
}