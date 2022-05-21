using FluentValidation;

using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class AddRoomMeetingValidator : AbstractValidator<AddRoomMeetingCommand>
    {
        private readonly IUserValidator userValidator;
        private readonly IRoomValidator roomValidator;

        public AddRoomMeetingValidator(IUserValidator userValidator, IRoomValidator roomValidator)
        {
            this.userValidator = userValidator;
            this.roomValidator = roomValidator;
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync((model, m, c) => roomValidator.IsUserRoomOwner(model.RoomId, model.UserId, c))
                .WithMessage("Not Room Owner");
            RuleFor(x => x.RoomId).NotNull().NotEmpty().MustAsync((model, m, c) => roomValidator.IsActiveRoom(m, c)).WithMessage("Not Active Room");
            When(x => !x.StartNow, () =>
             {
                 RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime).WithMessage("End time must be greater than start time");
             });
            RuleFor(x => x.UserId).MustAsync((model, m, c) => roomValidator.IsUserCanCreateMeeting(model.UserId, model.StartTime, model.EndTime, c)).WithMessage((model) => roomValidator.GetOverlappedMeetings(model.UserId, model.StartTime, model.EndTime));
        }
    }
}
