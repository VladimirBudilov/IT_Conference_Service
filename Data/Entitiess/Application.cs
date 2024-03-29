using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Service.Data.Entitiess
{
    [Table("applications")]
    public class Application : BaseEnity
    {
        

        [Required]
        [Column("author_id")]
        public Guid AuthorId { get; set; }

        [Required]
        [Column("is_sent", TypeName = "boolean")]
        public bool IsSent { get; set; } = false;

        [Required]
        [Column("updated_at", TypeName = "timestamp(0)")]
        public DateTime UpdatedAt { get; set; }
        
        [Column("sent_at", TypeName = "timestamp(0)")]
        public DateTime SentAt { get; set; }

        [Required]
        [Column("application_info_id")]
        [ForeignKey(nameof(ApplicationInfo))]
        public Guid ApplicationInfoId { get; set; }
        public ApplicationInfo ApplicationInfo { get; set; }
    }
}
