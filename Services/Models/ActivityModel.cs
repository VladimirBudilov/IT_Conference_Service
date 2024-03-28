using IT_Conference_Service.Data.Entitiess;
using System.Text.Json.Serialization;

namespace IT_Conference_Service.Services.Models
{
    public class ActivityModel
    {
        [JsonPropertyName("activity")]
        public ActivityTypeEnum Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
