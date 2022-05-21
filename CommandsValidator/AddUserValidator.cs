using FluentValidation;

using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IUserValidator userValidator;

        public AddUserValidator(IUserValidator userValidator)
        {
            this.userValidator = userValidator;
            RuleFor(x => x.Phonenumber)
                .NotEmpty()
                .MustAsync((phonenumber, token) => userValidator.IsAvailablePhoneNumber(phonenumber, token))
                .WithMessage("Phonenumber already exist");
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MustAsync((email, token) => userValidator.IsAvailableEmail(email, token))
                .WithMessage("Email already exist");
        }
    }
}
