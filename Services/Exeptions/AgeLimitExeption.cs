namespace Services.Exeptions
{
    public class AgeLimitException : Exception
    {
        public AgeLimitException(string message) : base(message) {}
    }
}
