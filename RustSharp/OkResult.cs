namespace RustSharp;

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

    /// <inheritdoc/>
    public override bool IsOk => true;

    /// <inheritdoc/>
    public override bool IsErr => false;

    internal OkResult(TValue value)
    {
        Value = value;
    }

    /// <inheritdoc/>
    public override bool IsOkAnd(Func<TValue, bool> f) => f.Invoke(Value);
    /// <inheritdoc/>
    public override bool IsErrAnd(Func<TError, bool> f) => false;
    /// <inheritdoc/>
    public override Option<TValue> Ok() => Option<TValue>.Some(Value);
    /// <inheritdoc/>
    public override Option<TError> Err() => Option<TError>.None();
    /// <inheritdoc/>
    public override Result<TNewValue, TError> Map<TNewValue>(Func<TValue, TNewValue> mapper) => Result<TNewValue, TError>.Ok(mapper.Invoke(Value));
    /// <inheritdoc/>
    public override TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> mapper) => mapper.Invoke(Value);
    /// <inheritdoc/>
    public override TNewValue MapOrElse<TNewValue>(Func<TError, TNewValue> errorMapper, Func<TValue, TNewValue> mapper) => mapper.Invoke(Value);
    /// <inheritdoc/>
    public override Result<TValue, TNewError> MapErr<TNewError>(Func<TError, TNewError> mapper) => Result<TValue, TNewError>.Ok(Value);
    /// <inheritdoc/>
    public override Result<TValue, TError> Inspect(Action<TValue> action)
    {
        action.Invoke(Value);

        return this;
    }

    /// <inheritdoc/>
    public override Result<TValue, TError> InspectErr(Action<TError> action) => this;
    /// <inheritdoc/>
    public override TValue Expect(string msg) => Value;
    /// <inheritdoc/>
    public override TValue Unwrap() => Value;
    /// <inheritdoc/>
    public override TValue? UnwrapOrDefault() => Value;
    /// <inheritdoc/>
    public override TError ExpectErr(string msg) => throw new UnwrapException(msg);
    /// <inheritdoc/>
    public override TError UnwrapErr() => throw new UnwrapException("Called Result.UnwrapErr on an Ok value");
    /// <inheritdoc/>
    public override Result<TNewValue, TError> And<TNewValue>(Result<TNewValue, TError> res) => res;
    /// <inheritdoc/>
    public override Result<TNewValue, TError> AndThen<TNewValue>(Func<TValue, Result<TNewValue, TError>> operation) => operation.Invoke(Value);
    /// <inheritdoc/>
    public override Result<TValue, TError> Or(Result<TValue, TError> res) => this;
    /// <inheritdoc/>
    public override Result<TValue, TNewError> OrElse<TNewError>(Func<TError, Result<TValue, TNewError>> operation) => Result<TValue, TNewError>.Ok(Value);
    /// <inheritdoc/>
    public override TValue UnwrapOr(TValue defaultValue) => Value;
    /// <inheritdoc/>
    public override TValue UnwrapOrElse(Func<TError, TValue> operation) => Value;
    /// <inheritdoc/>
    public override bool Contains<TOther>(TOther x) => x.Equals(Value);
    /// <inheritdoc/>
    public override bool ContainsErr<TOther>(TOther f) => false;
    /// <inheritdoc/>
    public override void Match(Action<TValue> okAction, Action<TError> errAction) => okAction.Invoke(Value);
    /// <inheritdoc/>
    public override Result<TValue, TError> Clone() => Value is ICloneable cloneableValue ? Ok((TValue)cloneableValue.Clone()) : Ok(Value);

    /// <summary>
    /// Convert from <see cref="Result.ValueOkResult{TValue}"/>
    /// </summary>
    /// <param name="valueOk"></param>
    public static implicit operator OkResult<TValue, TError>(Result.ValueOkResult<TValue> valueOk) => new OkResult<TValue, TError>(valueOk.Value);
}