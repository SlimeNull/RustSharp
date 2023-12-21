namespace RustSharp;

/// <summary>
/// Throws while calling 'Unwrap' on wrong value
/// </summary>
public class UnwrapException : Exception
{
    /// <summary>
    /// Initializes a new instance of the RustSharp.UnwrapException class.
    /// </summary>
    public UnwrapException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the RustSharp.UnwrapException class with a specified error message.
    /// </summary>
    /// <param name="message"></param>
    public UnwrapException(string? message) : base(message)
    {
    }
}