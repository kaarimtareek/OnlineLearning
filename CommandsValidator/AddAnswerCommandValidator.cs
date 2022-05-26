using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class AddAnswerCommandValidator : AbstractValidator<AddAnswerCommand>
    {
        public AddAnswerCommandValidator(IUserValidator userValidator, IRoomValidator roomValidator,IQuestionValidator questionValidator)
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync((x, m) => userValidator.IsActiveUserId(x, m));
            RuleFor(x => x.RoomId).NotNull().NotEmpty().MustAsync((x, m) => roomValidator.IsActiveRoom(x, m));
            RuleFor(x => x.QuestionId).NotNull().NotEmpty().MustAsync((x, m) => questionValidator.IsQuestionExist(x, m)).WithMessage("question not exist");
            RuleFor(x => x.RoomId).MustAsync((model, roomId, ct) => roomValidator.IsUserJoinedRoom(roomId, model.UserId, ct)).WithMessage("not joined in the room");


        }
    }
}
