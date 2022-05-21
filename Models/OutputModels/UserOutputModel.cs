
using System;

namespace OnlineLearning.Models.OutputModels
{
    public class UserOutputModel
    {
        public string Name { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }

        public string Id { get; set; }
    }
}