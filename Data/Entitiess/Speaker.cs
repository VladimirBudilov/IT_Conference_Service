using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Speaker__Service.Data.Entitiess
{
    public class Speaker : BaseEnity
    {
        [Required]
        [ForeignKey("ActivityType")]
        public Guid ActivityId { get; set; }
        public ActivityType ActivityType { get; set; }

        [Required]
        [ForeignKey("SpeackerInfo")]
        public Guid SpeackerInfoId { get; set; }
        public SpeackerInfo SpeackerInfo { get; set; }

    }
}
