namespace IT_Conference_Service.Validation
{
    public class ServiceBehaviorException : Exception
    {
        public ServiceBehaviorException()
        {
        }

        public ServiceBehaviorException(string message) : base(message)
        {
        }

        public ServiceBehaviorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
