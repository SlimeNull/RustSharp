namespace RustSharp
{
    /// <summary>
    /// <see cref="Result{TValue, TError}"/> is a type that represents either success (<see cref="OkResult{TValue, TError}"/>) or failure (<see cref="ErrResult{TValue, TError}"/>).
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public abstract class Result<TValue, TError> : ICloneable
    {
        public abstract bool IsOk { get; }
        public abstract bool IsErr { get; }

        public abstract bool IsOkAnd(Func<TValue, bool> f);

        public abstract bool IsErrAnd(Func<TError, bool> f);

        public abstract Option<TValue> Ok();

        public abstract Option<TError> Err();

        public abstract Result<TNewValue, TError> Map<TNewValue>(Func<TValue, TNewValue> mapper);

        public abstract TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> mapper);

        public abstract TNewValue MapOrElse<TNewValue>(Func<TError, TNewValue> errorMapper, Func<TValue, TNewValue> mapper);

        public abstract Result<TValue, TNewError> MapErr<TNewError>(Func<TError, TNewError> mapper);

        public abstract Result<TValue, TError> Inspect(Action<TValue> action);

        public abstract Result<TValue, TError> InspectErr(Action<TError> action);

        public abstract TValue Expect(string msg);

        public abstract TValue Unwrap();

        public abstract TValue? UnwrapOrDefault();

        public abstract TError ExpectErr(string msg);

        public abstract TError UnwrapErr();

        public abstract Result<TNewValue, TError> And<TNewValue>(Result<TNewValue, TError> res);

        public abstract Result<TNewValue, TError> AndThen<TNewValue>(Func<TValue, Result<TNewValue, TError>> operation);

        public abstract Result<TValue, TError> Or(Result<TValue, TError> res);

        public abstract Result<TValue, TNewError> OrElse<TNewError>(Func<TError, Result<TValue, TNewError>> operation);

        public abstract TValue UnwrapOr(TValue defaultValue);

        public abstract TValue UnwrapOrElse(Func<TError, TValue> operation);

        public abstract bool Contains<TOther>(TOther x) where TOther : IEquatable<TValue>;

        public abstract bool ContainsErr<TOther>(TOther f) where TOther : IEquatable<TError>;

        public abstract void Match(Action<TValue> okAction, Action<TError> errAction);

        public abstract Result<TValue, TError> Clone();

        public static OkResult<TValue, TError> Ok(TValue value) => new OkResult<TValue, TError>(value);
        public static ErrResult<TValue, TError> Err(TError error) => new ErrResult<TValue, TError>(error);

        public static implicit operator Result<TValue, TError>(Result.ValueOkResult<TValue> valueOk) => new OkResult<TValue, TError>(valueOk.Value);
        public static implicit operator Result<TValue, TError>(Result.ValueErrResult<TError> valueErr) => new ErrResult<TValue, TError>(valueErr.Value);

        object ICloneable.Clone() => Clone();
    }

    public static class Result
    {
        public static ValueOkResult<TValue> Ok<TValue>(TValue value) => new ValueOkResult<TValue>(value);
        public static ValueErrResult<TError> Err<TError>(TError error) => new ValueErrResult<TError>(error);

        public record struct ValueOkResult<TValue>(TValue Value);
        public record struct ValueErrResult<TValue>(TValue Value);
    }
}