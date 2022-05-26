using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class AddQuestionCommandValidator : AbstractValidator<AddQuestionCommand>
    {
        public AddQuestionCommandValidator(IUserValidator userValidator , IRoomValidator roomValidator)
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync((x, m) => userValidator.IsActiveUserId(x, m)).WithMessage("not active user");
            RuleFor(x => x.RoomId).NotNull().NotEmpty().MustAsync((x, m) => roomValidator.IsActiveRoom(x, m)).WithMessage("not active room");
            RuleFor(x => x.RoomId).MustAsync((model,roomId,ct) => roomValidator.IsUserJoinedRoom(roomId,model.UserId,ct)).WithMessage("not joined in the room");


        }
    }
}
