﻿using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.InputModels
{
    public class EditQuestionInputModel
    {
        [Required]
        public string QuestionTitle { get; set; }
        [Required]
        public string QuestionDescription { get; set; }
    }
}
