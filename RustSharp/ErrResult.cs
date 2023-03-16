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

        public override bool IsOk => false;

        public override bool IsErr => true;

        internal ErrResult(TError error)
        {
            Value = error;
        }

        public override bool IsOkAnd(Func<TValue, bool> f) => false;
        public override bool IsErrAnd(Func<TError, bool> f) => f.Invoke(Value);
        public override Option<TValue> Ok() => Option<TValue>.None();
        public override Option<TError> Err() => Option<TError>.Some(Value);
        public override Result<TNewValue, TError> Map<TNewValue>(Func<TValue, TNewValue> mapper) => Result<TNewValue, TError>.Err(Value);
        public override TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> mapper) => defaultValue;
        public override TNewValue MapOrElse<TNewValue>(Func<TError, TNewValue> errorMapper, Func<TValue, TNewValue> mapper) => errorMapper.Invoke(Value);
        public override Result<TValue, TNewError> MapErr<TNewError>(Func<TError, TNewError> mapper) => Result<TValue, TNewError>.Err(mapper.Invoke(Value));
        public override Result<TValue, TError> Inspect(Action<TValue> action) => this;
        public override Result<TValue, TError> InspectErr(Action<TError> action)
        {
            action.Invoke(Value);

            return this;
        }

        public override TValue Expect(string msg) => throw UnwrapException.New(msg, Value);
        public override TValue Unwrap() => throw UnwrapException.New("Called Result.Unwrap on an Err value", Value);
        public override TValue? UnwrapOrDefault() => default;
        public override TError ExpectErr(string msg) => Value;
        public override TError UnwrapErr() => Value;
        public override Result<TNewValue, TError> And<TNewValue>(Result<TNewValue, TError> res) => Result<TNewValue, TError>.Err(Value);
        public override Result<TNewValue, TError> AndThen<TNewValue>(Func<TValue, Result<TNewValue, TError>> operation) => Result<TNewValue, TError>.Err(Value);
        public override Result<TValue, TError> Or(Result<TValue, TError> res) => res;
        public override Result<TValue, TNewError> OrElse<TNewError>(Func<TError, Result<TValue, TNewError>> operation) => operation.Invoke(Value);
        public override TValue UnwrapOr(TValue defaultValue) => defaultValue;
        public override TValue UnwrapOrElse(Func<TError, TValue> operation) => operation.Invoke(Value);
        public override bool Contains<TOther>(TOther x) => false;
        public override bool ContainsErr<TOther>(TOther f) => f.Equals(Value);
        public override void Match(Action<TValue> okAction, Action<TError> errAction) => errAction.Invoke(Value);
        public override Result<TValue, TError> Clone() => Value is ICloneable cloneableValue ? Err((TError)cloneableValue.Clone()) : Err(Value);
    }
}