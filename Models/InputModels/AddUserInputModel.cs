using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.InputModels
{
    public class AddUserInputModel
    {
        [Required]
        [MaxLength(200)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string Phonenumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public DateTime? BrithDate { get; set; }
        [MinLength(9)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}