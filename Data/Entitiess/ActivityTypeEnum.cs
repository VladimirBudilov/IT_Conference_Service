using System.Runtime.Serialization;

namespace IT_Conference_Service.Data.Entitiess
{
    public enum ActivityTypeEnum
    {
        [EnumMember(Value = "Lecture")]
        Lecture,

        [EnumMember(Value = "Workshop")]
        Workshop,

        [EnumMember(Value = "Discussion")]
        Discussion
    }
}
