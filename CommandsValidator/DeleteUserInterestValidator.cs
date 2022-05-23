using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class DeleteUserInterestValidator : AbstractValidator<DeleteUserInterestCommand>
    {
        public DeleteUserInterestValidator(IInterestValidator interestValidator,IUserValidator userValidator)
        {
            RuleFor(x=>x.InterestId).NotEmpty().MustAsync((x,m)=> interestValidator.IsInterestExist(x)).WithMessage("Interest Not Found");
            RuleFor(x=>x.UserId).NotEmpty().MustAsync((x,m)=> userValidator.IsActiveUserId(x,m)).WithMessage("Not Active User");

        }
    }
}
