using IT_Conference_Service.Helpers.Validation;
using System.ComponentModel;
using System.Reflection;

namespace IT_Conference_Service.Helpers.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value, string message = "string was not converted to enam") where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ServiceBehaviorException(message);
            }

            foreach (T item in Enum.GetValues(typeof(T)))
            {
                FieldInfo fi = typeof(T).GetField(item.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes[0].Description == value)
                    return item;
            }

            throw new ServiceBehaviorException(message);
        }

        public static string EnumToString<T>(this T value,string message = "enam was not converted to string") where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ServiceBehaviorException(message);
            }

            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static bool CanConvertToEnum<T>(this string value, string message) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ServiceBehaviorException(message);
            }

            foreach (T item in Enum.GetValues(typeof(T)))
            {
                FieldInfo fi = typeof(T).GetField(item.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes[0].Description == value)
                    return true;
            }

            return false;
        }
    }
}
