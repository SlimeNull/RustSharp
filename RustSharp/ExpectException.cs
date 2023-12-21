namespace RustSharp;

/// <summary>
/// Throws while calling 'Except' on wrong value
/// </summary>
public class ExpectException : Exception
{
    /// <summary>
    /// Initializes a new instance of the RustSharp.ExpectException class.
    /// </summary>
    public ExpectException()
    {
    }


    /// <summary>
    /// Initializes a new instance of the RustSharp.ExpectException class with a specified error message.
    /// </summary>
    /// <param name="message"></param>
    public ExpectException(string? message) : base(message)
    {
    }
}