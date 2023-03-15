namespace RustSharp
{
    /// <summary>
    /// Some value of type <see cref="TValue"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class SomeOption<TValue> : Option<TValue>
    {
        /// <summary>
        /// Value
        /// </summary>
        public TValue Value { get; }

        internal SomeOption(TValue value)
        {
            Value = value;
        }
    }
}