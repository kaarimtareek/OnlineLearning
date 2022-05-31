using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class EditQuestionCommandValidator : AbstractValidator<EditQuestionCommand>
    {
        public EditQuestionCommandValidator(IUserValidator userValidator, IQuestionValidator questionValidator)
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync((x, m) => userValidator.IsActiveUserId(x, m));
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync((model, x, m) => questionValidator.IsQuestionOwner(model.QuestionId, x, m)).WithMessage("not question owner");
            RuleFor(x => x.QuestionId).NotNull().NotEmpty().MustAsync((x, m) => questionValidator.IsQuestionExist(x, m)).WithMessage("question not exist");


        }
    }
}
