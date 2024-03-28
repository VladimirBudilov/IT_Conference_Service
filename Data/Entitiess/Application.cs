using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Service.Data.Entitiess
{
    public class Application : BaseEnity
    {
        [Required]
        [Column(TypeName = "int")]
        public ActivityTypeEnum ActivityType { get; set; }

        [Required]
        [ForeignKey(nameof(AuthorInfo))]
        public Guid AuthorInfoId { get; set; }
        public AuthorInfo AuthorInfo { get; set; }

        [Required]
        [Column(TypeName = "bool")]
        public bool IsSent { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
