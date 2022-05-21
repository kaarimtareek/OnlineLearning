using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.InputModels
{
    public class AddRoomMaterialInputModel
    {
        [MaxLength(200)]
        public string FileName { get; set; }
    }
}
