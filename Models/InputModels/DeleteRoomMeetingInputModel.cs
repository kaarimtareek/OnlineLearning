using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.InputModels
{
    public class DeleteRoomMeetingInputModel
    {
        [Required]
        public string ZoomToken { get; set; }
    }
}
