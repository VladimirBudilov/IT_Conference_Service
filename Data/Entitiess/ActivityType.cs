using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Speaker__Service.Data.Entitiess
{
    public class ActivityType : BaseEnity
    {
        [Required]
        [Column(TypeName = "int")]
        public ActivityTypeEnum Name { get; set; }
    }
}
