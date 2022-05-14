using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class UpdateRoomMeetingValidator : AbstractValidator<UpdateRoomMeetingCommand>
    {
        public UpdateRoomMeetingValidator(IRoomValidator roomValidator, IUserValidator userValidator)
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync((model, m, c) => roomValidator.IsUserRoomOwner(model.RoomId, model.UserId, c))
               .WithMessage("Not Room Owner");
            RuleFor(x => x.RoomId).NotNull().NotEmpty().MustAsync((model, m, c) => roomValidator.IsActiveRoom(m, c)).WithMessage("Not Active Room");
            When(x => !x.StartNow, () =>
               {
                   RuleFor(x => x.EndDate).NotNull();
                   RuleFor(x => x.StartDate).NotNull().MustAsync((model, d, c) => roomValidator.IsUserCanCreateMeeting(model.UserId, model.StartDate.Value, model.EndDate.Value, c,model.MeetingId)).WithMessage((model)=>  roomValidator.GetOverlappedMeetings(model.UserId,model.StartDate.Value,model.EndDate.Value,model.MeetingId));
               });
        }
    }
}
