using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IT_Conference_Service.Data.Entitiess
{
    public class AuthorInfo : BaseEnity
    {
        [Required]
        [Column(TypeName = "text")]
        [MaxLength(100)]
        public string ActivityName { get; set; }

        [Column(TypeName = "text")]
        [MaxLength(300)]
        public string? Description { get; set; }

        [Column(TypeName = "text")]
        [MaxLength(1000)]
        public string Plan { get; set; }

        public Application Application { get; set; }
    }
}
