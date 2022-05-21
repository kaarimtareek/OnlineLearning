
using FluentValidation;

using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class AddRoomMaterialValidator : AbstractValidator<AddRoomMaterialCommand>
    {
        private readonly IUserValidator userValidator;
        private readonly IRoomValidator roomValidator;

        public AddRoomMaterialValidator(IUserValidator userValidator, IRoomValidator roomValidator)
        {
            this.userValidator = userValidator;
            this.roomValidator = roomValidator;
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync((model, m, c) => roomValidator.IsUserRoomOwner(model.RoomId, model.UserId, c))
                .WithMessage("Not Room Owner");
        }

    }
}
