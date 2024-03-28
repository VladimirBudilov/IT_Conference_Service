using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Service.Data.Entitiess
{
    public class BaseEnity
    {
        [Key]
        [Required]
        [Column("id", TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }
    }
}
