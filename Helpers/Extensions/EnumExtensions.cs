using IT_Conference_Service.Helpers.Validation;

namespace IT_Conference_Service.Helpers.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value, string message) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ServiceBehaviorException(message);
            }

            if (Enum.TryParse(value, true, out T result))
            {
                return result;
            }

            throw new ServiceBehaviorException(message);
        }

        public static string EnumToString<T>(this T value,string message) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ServiceBehaviorException(message);
            }

            return value.ToString();
        }

        public static bool CanConvertToEnum<T>(this string value, string message) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ServiceBehaviorException(message);
            }

            return Enum.GetNames(typeof(T)).Any(e => e.Equals(value, StringComparison.OrdinalIgnoreCase));
        }
    }
}
