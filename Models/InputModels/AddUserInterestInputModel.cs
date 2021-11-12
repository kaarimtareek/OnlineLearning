using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Models.InputModels
{
    public class AddUserInterestInputModel
    {
        public string InterestId { get; set; }
        public string Interest { get; set; }
        public bool IgnoreSimilarity { get; set; }
    }
}
