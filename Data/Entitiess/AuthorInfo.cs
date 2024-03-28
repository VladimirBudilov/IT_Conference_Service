using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Service.Data.Entitiess
{
    public class AuthorInfo : BaseEnity
    {
        [Required]
        [Column(TypeName = "text")]
        [MaxLength(100)]
        public string PresentationName { get; set; }

        [Column(TypeName = "text")]
        [MaxLength(300)]
        public string? DescriptionForWebsie { get; set; }
        [Column(TypeName = "text")]
        [MaxLength(1000)]
        public string Plan { get; set; }
    }
}
