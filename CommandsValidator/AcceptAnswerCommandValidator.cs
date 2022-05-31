using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class AcceptAnswerCommandValidator : AbstractValidator<AcceptAnswerCommand>
    {
        public AcceptAnswerCommandValidator(IUserValidator userValidator, IQuestionValidator questionValidator)
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync((x, m) => userValidator.IsActiveUserId(x, m));
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync((model, x, m) => questionValidator.IsQuestionOwnerForAnswer(model.AnswerId, x, m)).WithMessage("not question owner");
            RuleFor(x => x.AnswerId).NotNull().NotEmpty().MustAsync((x, m) => questionValidator.IsAnswerExist(x, m)).WithMessage("answer not exist");


        }
    }
}
