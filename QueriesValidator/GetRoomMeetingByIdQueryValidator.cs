using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;
using OnlineLearning.Queries;

namespace OnlineLearning.QueriesValidator
{
    public class GetRoomMeetingByIdQueryValidator : AbstractValidator<GetRoomMeetingByIdQuery>
    {

        public GetRoomMeetingByIdQueryValidator(IUserValidator userValidator)
        {
            RuleFor(x => x.UserId).MustAsync((model, id, cancelationToken) => userValidator.IsActiveUserId(model.UserId,cancelationToken))
                .WithMessage("User is not active");
        }

    }
}
