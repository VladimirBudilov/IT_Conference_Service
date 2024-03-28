using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Service.Data.Entitiess
{
    [Table("applications")]
    public class Application : BaseEnity
    {
        [Required]
        [Column("activity_type", TypeName = "int")]

        public ActivityTypeEnum ActivityType { get; set; }

        [Required]
        [Column("author_id", TypeName = "uniqueidentifier")]
        public Guid AuthorId { get; set; }

        [Required]
        [Column("is_sent", TypeName = "bool")]
        public bool IsSent { get; set; } = false;

        [Required]
        [Column("created_at", TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationInfo))]
        [Column("application_info_id", TypeName = "uniqueidentifier")]
        public Guid ApplicationInfoId { get; set; }
        public ApplicationInfo ApplicationInfo { get; set; }
    }
}
