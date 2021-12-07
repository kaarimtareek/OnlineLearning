using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Models.OutputModels
{
    public class InterestOutputModel
    {
        public string Id { get; set; }
        public string StemmedName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
