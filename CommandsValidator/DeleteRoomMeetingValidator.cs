using FluentValidation;
using OnlineLearning.Commands;
using OnlineLearning.EntitiesValidators;

namespace OnlineLearning.CommandsValidator
{
    public class DeleteRoomMeetingValidator : AbstractValidator<DeleteRoomMeetingCommand>
    {
        public DeleteRoomMeetingValidator(IRoomValidator roomValidator, IMeetingValidator meetingValidator)
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().MustAsync((model, m, c) => roomValidator.IsUserRoomOwner(model.RoomId, model.UserId, c))
               .WithMessage("Not Room Owner");
            RuleFor(x => x.RoomId).NotNull().NotEmpty().MustAsync((model, m, c) => roomValidator.IsActiveRoom(m, c)).WithMessage("Not Active Room");
            RuleFor(x => x.MeetingId).NotNull().NotEmpty().MustAsync((model, m, c) => meetingValidator.IsMeetingExist(m,c)).WithMessage("Room meeting not found");
           
        }
    }
}
