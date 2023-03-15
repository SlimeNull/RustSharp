using System.Runtime.Serialization;

namespace RustSharp
{
    public class ExpectException : Exception
    {
        public ExpectException()
        {
        }

        public ExpectException(string? message) : base(message)
        {
        }

        public ExpectException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ExpectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static ExpectException New(string msg) => new ExpectException(msg);
    }
}