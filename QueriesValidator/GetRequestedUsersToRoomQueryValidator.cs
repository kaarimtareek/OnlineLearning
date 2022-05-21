using FluentValidation;

using OnlineLearning.EntitiesValidators;
using OnlineLearning.Queries;

namespace OnlineLearning.QueriesValidator
{
    public class GetRequestedUsersToRoomQueryValidator : AbstractValidator<GetRequestedUsersToRoomQuery>
    {
        private readonly IRoomValidator roomValidator;

        public GetRequestedUsersToRoomQueryValidator(IRoomValidator roomValidator)
        {
            this.roomValidator = roomValidator;
            RuleFor(x => x.RoomOwnerId).MustAsync((model, id, cancelationToken) => roomValidator.IsUserRoomOwner(model.RoomId, id, cancelationToken))
                .WithMessage("User is not the owner of the Room");
        }

    }
}
