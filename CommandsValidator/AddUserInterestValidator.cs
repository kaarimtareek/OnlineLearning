using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.CommandsValidator
{
    public class AddUserInterestValidator : AbstractValidator<AddUserInterestCommand>
    {
        private readonly IUserValidator userValidator;

        public AddUserInterestValidator(IUserValidator userValidator)
        {
            this.userValidator = userValidator;
            RuleFor(x => x.UserId)
                .NotEmpty()
                .MustAsync((id,cancellationToken) => userValidator.IsExistingUserId(id,cancellationToken))
                .WithMessage("User doesn't exist");
        }
    }
}
