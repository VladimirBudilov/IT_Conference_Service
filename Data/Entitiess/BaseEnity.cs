using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Service.Data.Entitiess
{
    public class BaseEnity
    {
        [Key]
        [Required]
        [Column("id")]
        public Guid Id { get; set; }
    }
}
