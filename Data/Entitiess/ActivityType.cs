using System.ComponentModel;

namespace IT_Conference_Service.Data.Entitiess
{
    public enum ActivityType
    {
        [Description("Lecture")]
        Lecture,

        [Description("Workshop")]
        Workshop,

        [Description("Discussion")]
        Discussion,

        [Description("None")]
        None
    }
}
