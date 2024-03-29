using System.Reflection;
using System.Runtime.Serialization;

namespace IT_Conference_Service.Data.Entitiess
{
    public enum ActivityType
    {
        [EnumMember(Value = "Lecture")]
        Lecture,

        [EnumMember(Value = "Workshop")]
        Workshop,

        [EnumMember(Value = "Discussion")]
        Discussion
    }

    public static class ActivityTypeExtensions
    {
        public static string ToEnumMemberString(this ActivityType activityType)
        {
            var type = typeof(ActivityType);
            var memberInfos = type.GetMember(activityType.ToString());
            var enumMemberAttribute = memberInfos[0].GetCustomAttribute<EnumMemberAttribute>(false);

            return enumMemberAttribute?.Value ?? activityType.ToString();
        }
    }
}
