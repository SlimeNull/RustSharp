namespace RustSharp
{
    /// <summary>
    /// Contains the error value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public class ErrResult<TValue, TError> : Result<TValue, TError>
    {
        /// <summary>
        /// The error value
        /// </summary>
        public TError Value { get; }

        internal ErrResult(TError error)
        {
            Value = error;
        }
    }
}