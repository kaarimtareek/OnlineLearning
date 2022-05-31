using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.InputModels
{
    public class EditAnswerInputModel
    {
        [Required]
        public string AnswerDescription { get; set; }
    }
}
