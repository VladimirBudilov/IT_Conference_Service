namespace IT_Conference_Service.Helpers.Validation
{
    public class DatabaseBehaviorException : Exception
    {
        public DatabaseBehaviorException(string message) : base(message)
        {
        }

        public DatabaseBehaviorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DatabaseBehaviorException()
        {
        }
    }
}
