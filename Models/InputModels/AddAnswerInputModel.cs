using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.InputModels
{
    public class AddAnswerInputModel
    {
        [Required]
        public string AnswerDescription { get; set; }
    }
}
