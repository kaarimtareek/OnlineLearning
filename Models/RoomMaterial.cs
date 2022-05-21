using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearning.Models
{
    public class RoomMaterial : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomId { get; set; }
        [MaxLength(500)]
        public string FilePath { get; set; }
        [MaxLength(500)]
        public string FileName { get; set; }
        public bool IsActive { get; set; }
        public virtual Room Room { get; set; }
    }
}
