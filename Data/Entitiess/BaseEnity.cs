using System.ComponentModel.DataAnnotations;

namespace IT_Conference_Service.Data.Entitiess
{
    public class BaseEnity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
