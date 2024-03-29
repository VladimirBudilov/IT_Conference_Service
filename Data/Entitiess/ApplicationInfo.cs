using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Service.Data.Entitiess
{
    [Table("application_info")]
    public class ApplicationInfo : BaseEnity
    {
        [Required]
        [Column("activity_type", TypeName = "int")]
        public ActivityType ActivityType { get; set; }

        [Required]
        [Column("activity_name", TypeName = "text")]
        [MaxLength(100)]
        public string ActivityName { get; set; }

        [Column("description", TypeName = "text")]
        [MaxLength(300)]
        public string? Description { get; set; }

        [Required]
        [Column("outline", TypeName = "text")]
        [MaxLength(1000)]
        public string Outline { get; set; }

        public Application Application { get; set; }
    }
}
