using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Models
{
    [Table("LOOKUP_USER_STATUS")]
    public class LookupUserStatus : BaseEntity
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string NameArabic { get; set; }
        [MaxLength(100)]
        public string NameEnglish { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
