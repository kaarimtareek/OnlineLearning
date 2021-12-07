using FluentValidation;

using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

using System;

namespace OnlineLearning.CommandsValidator
{
    public class AddRoomValidator : AbstractValidator<AddRoomCommand>
    {
        private readonly IUserValidator userValidator;
        private readonly IInterestValidator interestValidator;

        public AddRoomValidator(IUserValidator userValidator, IInterestValidator interestValidator)
        {
            this.userValidator = userValidator;
            this.interestValidator = interestValidator;
            

            RuleFor(x => x.UserId)
                .NotEmpty()
                .MustAsync((id, cancellationToken) => userValidator.IsExistingUserId(id, cancellationToken))
                .WithMessage("User doesn't exist");
            RuleFor(x => x.Interests)
                .NotEmpty()
                .WithMessage("Cannot Create Room with no interest")
                .Must((x) => interestValidator.IsAllInterestsExist(x))
                .WithMessage("Invalid Interests");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Invalid Price");
            RuleFor(x => x.RoomName)
                .NotEmpty()
                .WithMessage("Room name cannot be empty");
            RuleFor(x => x.RoomDescription)
                .NotEmpty()
                .WithMessage("Room description cannot be empty");
            RuleFor(x => x.StartDate)
                .GreaterThan(DateTime.Now.AddHours(2))
                .WithMessage("Cannot create room with start date less than 2 hours from now");
            RuleFor(x => x.ExpectedEndDate)
                .GreaterThan(x => x.StartDate.AddHours(1))
                .WithMessage("cannot add expected end date less than 1 hour from start date");
        }
    }
}