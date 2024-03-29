using IT_Conference_Service.Data.Entitiess;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IT_Conference_Service.Services.Models
{
    public class ApplicationModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("author")]
        [Required]
        [RegularExpression(@"^[A-Fa-f0-9]{8}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{12}$")]
        public Guid AuthorId { get; set; }

        [JsonPropertyName("type")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string ActivityType { get; set; }

        [JsonPropertyName("name")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string ActivityName { get; set; }

        [JsonPropertyName("description")]
        [DataType(DataType.Text)]
        [MaxLength(300)]
        public string Description { get; set; }

        [JsonPropertyName("outline")]
        [DataType(DataType.Text)]
        [MaxLength(1000)]
        public string Outline { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
    }
}
