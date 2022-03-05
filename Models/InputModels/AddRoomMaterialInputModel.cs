using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Models.InputModels
{
    public class AddRoomMaterialInputModel
    {
        [MaxLength(200)]
        public string FileName { get; set; }
    }
}
