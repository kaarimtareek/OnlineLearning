using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.InputModels
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
        public string Password { get; set; }
    }
}
