using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IT_Conference_Service.Data.Entitiess
{
    public class Application : BaseEnity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public ActivityTypeEnum Name { get; set; }

        [Required]
        [ForeignKey("SpeakerInfo")]
        public Guid SpeakerInfoId { get; set; }
        public SpeakerInfo SpeackerInfo { get; set; }

        [Required]
        [Column(TypeName = "bool")]
        public bool IsAccepted { get; set; }
    }
}
