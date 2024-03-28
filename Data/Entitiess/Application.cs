using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Service.Data.Entitiess
{
    public class Application : BaseEnity
    {
        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public ActivityTypeEnum ActivityType { get; set; }

        [Required]
        [ForeignKey("AuthorInfo")]
        public Guid AuthorInfoId { get; set; }
        public AuthorInfo AuthorInfo { get; set; }

        [Required]
        [Column(TypeName = "bool")]
        public bool IsSent { get; set; } = false;
    }
}
