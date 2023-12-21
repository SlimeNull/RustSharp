namespace RustSharp;

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

    /// <inheritdoc/>
    public override bool IsOk => false;

    /// <inheritdoc/>
    public override bool IsErr => true;

    internal ErrResult(TError error)
    {
        Value = error;
    }

    /// <inheritdoc/>
    public override bool IsOkAnd(Func<TValue, bool> f) => false;
    /// <inheritdoc/>
    public override bool IsErrAnd(Func<TError, bool> f) => f.Invoke(Value);
    /// <inheritdoc/>
    public override Option<TValue> Ok() => Option<TValue>.None();
    /// <inheritdoc/>
    public override Option<TError> Err() => Option<TError>.Some(Value);
    /// <inheritdoc/>
    public override Result<TNewValue, TError> Map<TNewValue>(Func<TValue, TNewValue> mapper) => Result<TNewValue, TError>.Err(Value);
    /// <inheritdoc/>
    public override TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> mapper) => defaultValue;
    /// <inheritdoc/>
    public override TNewValue MapOrElse<TNewValue>(Func<TError, TNewValue> errorMapper, Func<TValue, TNewValue> mapper) => errorMapper.Invoke(Value);
    /// <inheritdoc/>
    public override Result<TValue, TNewError> MapErr<TNewError>(Func<TError, TNewError> mapper) => Result<TValue, TNewError>.Err(mapper.Invoke(Value));
    /// <inheritdoc/>
    public override Result<TValue, TError> Inspect(Action<TValue> action) => this;
    /// <inheritdoc/>
    public override Result<TValue, TError> InspectErr(Action<TError> action)
    {
        action.Invoke(Value);

        return this;
    }

    /// <inheritdoc/>
    public override TValue Expect(string msg) => throw new ExpectException(msg);
    /// <inheritdoc/>
    public override TValue Unwrap() => throw new UnwrapException("Called Result.Unwrap on an Err value");
    /// <inheritdoc/>
    public override TValue? UnwrapOrDefault() => default;
    /// <inheritdoc/>
    public override TError ExpectErr(string msg) => Value;
    /// <inheritdoc/>
    public override TError UnwrapErr() => Value;
    /// <inheritdoc/>
    public override Result<TNewValue, TError> And<TNewValue>(Result<TNewValue, TError> res) => Result<TNewValue, TError>.Err(Value);
    /// <inheritdoc/>
    public override Result<TNewValue, TError> AndThen<TNewValue>(Func<TValue, Result<TNewValue, TError>> operation) => Result<TNewValue, TError>.Err(Value);
    /// <inheritdoc/>
    public override Result<TValue, TError> Or(Result<TValue, TError> res) => res;
    /// <inheritdoc/>
    public override Result<TValue, TNewError> OrElse<TNewError>(Func<TError, Result<TValue, TNewError>> operation) => operation.Invoke(Value);
    /// <inheritdoc/>
    public override TValue UnwrapOr(TValue defaultValue) => defaultValue;
    /// <inheritdoc/>
    public override TValue UnwrapOrElse(Func<TError, TValue> operation) => operation.Invoke(Value);
    /// <inheritdoc/>
    public override bool Contains<TOther>(TOther x) => false;
    /// <inheritdoc/>
    public override bool ContainsErr<TOther>(TOther f) => f.Equals(Value);
    /// <inheritdoc/>
    public override void Match(Action<TValue> okAction, Action<TError> errAction) => errAction.Invoke(Value);
    /// <inheritdoc/>
    public override Result<TValue, TError> Clone() => Value is ICloneable cloneableValue ? Err((TError)cloneableValue.Clone()) : Err(Value);

    /// <summary>
    /// Convert from <see cref="Result.ValueErrResult{TValue}"/>
    /// </summary>
    /// <param name="valueErr"></param>

    public static implicit operator ErrResult<TValue, TError>(Result.ValueErrResult<TError> valueErr) => new ErrResult<TValue, TError>(valueErr.Value);
}