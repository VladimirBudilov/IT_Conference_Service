using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IT_Conference_Service.Services.Models
{
    public class ApplicationModel
    {
        [JsonPropertyName("id")]
        [SwaggerSchema(ReadOnly = true)]
        public Guid Id { get; set; }

        [JsonPropertyName("author")]
        [Required]
        [RegularExpression(@"^[A-Fa-f0-9]{8}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{4}-[A-Fa-f0-9]{12}$")]
        public Guid AuthorId { get; set; }

        [JsonPropertyName("type")]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [DefaultValue("Lecture")]
        public string ActivityType { get; set; }

        [JsonPropertyName("name")]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        [DefaultValue("will talk about")]
        public string ActivityName { get; set; }

        [JsonPropertyName("description")]
        [DataType(DataType.Text)]
        [MaxLength(300)]
        [DefaultValue("description of what will be talked about ")]
        public string Description { get; set; }

        [JsonPropertyName("outline")]
        [DataType(DataType.Text)]
        [MaxLength(1000)]
        [DefaultValue("description of what will be talked about. a lot")]
        public string Outline { get; set; }

        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public DateTime SentAt { get; set; }
    }
}
