using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.InputModels
{
    public class AddUserInterestInputModel
    {
        [Required]
        public List<string>Interests{ get; set; }
    }
}
