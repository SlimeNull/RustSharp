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

        public override bool IsOk => true;

        public override bool IsErr => false;

        internal OkResult(TValue value)
        {
            Value = value;
        }

        public override bool IsOkAnd(Func<TValue, bool> f) => f.Invoke(Value);
        public override bool IsErrAnd(Func<TError, bool> f) => false;
        public override Option<TValue> Ok() => Option<TValue>.Some(Value);
        public override Option<TError> Err() => Option<TError>.None();
        public override Result<TNewValue, TError> Map<TNewValue>(Func<TValue, TNewValue> mapper) => Result<TNewValue, TError>.Ok(mapper.Invoke(Value));
        public override TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> mapper) => mapper.Invoke(Value);
        public override TNewValue MapOrElse<TNewValue>(Func<TError, TNewValue> errorMapper, Func<TValue, TNewValue> mapper) => mapper.Invoke(Value);
        public override Result<TValue, TNewError> MapErr<TNewError>(Func<TError, TNewError> mapper) => Result<TValue, TNewError>.Ok(Value);
        public override Result<TValue, TError> Inspect(Action<TValue> action)
        {
            action.Invoke(Value);

            return this;
        }

        public override Result<TValue, TError> InspectErr(Action<TError> action) => this;
        public override TValue Expect(string msg) => Value;
        public override TValue Unwrap() => Value;
        public override TValue? UnwrapOrDefault() => Value;
        public override TError ExpectErr(string msg) => throw UnwrapException.New(msg, Value);
        public override TError UnwrapErr() => throw UnwrapException.New("Called Result.UnwrapErr on an Ok value", Value);
        public override Result<TNewValue, TError> And<TNewValue>(Result<TNewValue, TError> res) => res;
        public override Result<TNewValue, TError> AndThen<TNewValue>(Func<TValue, Result<TNewValue, TError>> operation) => operation.Invoke(Value);
        public override Result<TValue, TError> Or(Result<TValue, TError> res) => this;
        public override Result<TValue, TNewError> OrElse<TNewError>(Func<TError, Result<TValue, TNewError>> operation) => Result<TValue, TNewError>.Ok(Value);
        public override TValue UnwrapOr(TValue defaultValue) => Value;
        public override TValue UnwrapOrElse(Func<TError, TValue> operation) => Value;
        public override bool Contains<TOther>(TOther x) => x.Equals(Value);
        public override bool ContainsErr<TOther>(TOther f) => false;
        public override void Match(Action<TValue> okAction, Action<TError> errAction) => okAction.Invoke(Value);
        public override Result<TValue, TError> Clone() => Value is ICloneable cloneableValue ? Ok((TValue)cloneableValue.Clone()) : Ok(Value);
    }
}