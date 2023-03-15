namespace RustSharp
{
    /// <summary>
    /// Contains the success value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public class OkResult<TValue, TError> : Result<TValue, TError>
    {
        /// <summary>
        /// The success value
        /// </summary>
        public TValue Value { get; }

        internal OkResult(TValue value)
        {
            Value = value;
        }
    }
}