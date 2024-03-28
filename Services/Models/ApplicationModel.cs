using IT_Conference_Service.Data.Entitiess;
using System.Text.Json.Serialization;

namespace IT_Conference_Service.Services.Models
{
    public class ApplicationModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("author")]
        public Guid AuthorId { get; set; }

        [JsonPropertyName("type")]
        public ActivityTypeEnum ActivityType { get; set; }

        [JsonPropertyName("name")]
        public string ActivityName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("outline")]
        public string Outline { get; set; }
    }
}
