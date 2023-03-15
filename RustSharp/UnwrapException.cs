using System.Runtime.Serialization;

namespace RustSharp
{
    public class UnwrapException : Exception
    {
        public UnwrapException()
        {
        }

        public UnwrapException(string? message) : base(message)
        {
        }

        public UnwrapException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnwrapException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static UnwrapException New(string msg, object? error)
        {
            return new UnwrapException($"{msg}: {error}");
        }
    }
}