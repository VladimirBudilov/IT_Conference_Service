using System.ComponentModel.DataAnnotations;

namespace IT_Conference_Speaker__Service.Data.Entitiess
{
    public class BaseEnity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
