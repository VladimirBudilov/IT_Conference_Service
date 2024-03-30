using IT_Conference_Service.Data.Entitiess;
using System.Text.Json.Serialization;

namespace IT_Conference_Service.Services.Models
{
    public class ActivityModel
    {
        [JsonPropertyName("activity")]
        public string Type { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        #region Equality and HashCode overrides
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var activity = (ActivityModel)obj;
            return Type == activity.Type && Description == activity.Description;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Type, Description);
        }
        #endregion
    }
}
