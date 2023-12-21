namespace RustSharp;

/// <summary>
/// Some value for <see cref="Option{TValue}"/>
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class SomeOption<TValue> : Option<TValue>
{
    /// <summary>
    /// Value
    /// </summary>
    public TValue Value { get; }

    /// <inheritdoc/>
    public override bool IsSome => true;

    /// <inheritdoc/>
    public override bool IsNone => false;

    internal SomeOption(TValue value)
    {
        Value = value;
    }

    /// <inheritdoc/>
    public override bool IsSomeAnd(Func<TValue, bool> f) => f.Invoke(Value);
    /// <inheritdoc/>
    public override TValue Expect(string msg) => Value;
    /// <inheritdoc/>
    public override TValue Unwrap() => Value;
    /// <inheritdoc/>
    public override TValue UnwrapOr(TValue defaultValue) => Value;
    /// <inheritdoc/>
    public override TValue UnwrapOrElse(Func<TValue> f) => Value;
    /// <inheritdoc/>
    public override TValue? UnwrapOrDefault() => Value;
    /// <inheritdoc/>
    public override Option<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> f) => Option<TNewValue>.Some(f.Invoke(Value));
    /// <inheritdoc/>
    public override Option<TValue> Inspect(Action<TValue> action)
    {
        action.Invoke(Value);

        return this;
    }

    /// <inheritdoc/>
    public override TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> f) => f.Invoke(Value);
    /// <inheritdoc/>
    public override TNewValue MapOrElse<TNewValue>(Func<TNewValue> defaultValueGetter, Func<TValue, TNewValue> f) => f.Invoke(Value);
    /// <inheritdoc/>
    public override Result<TValue, TError> OkOr<TError>(TError err) => Result<TValue, TError>.Ok(Value);
    /// <inheritdoc/>
    public override Result<TValue, TError> OkOrElse<TError>(Func<TError> errorGetter) => Result<TValue, TError>.Ok(Value);
    /// <inheritdoc/>
    public override Option<TNewValue> And<TNewValue>(Option<TNewValue> optb) => optb;
    /// <inheritdoc/>
    public override Option<TNewValue> AndThen<TNewValue>(Func<TValue, TNewValue> f) => Option<TNewValue>.Some(f.Invoke(Value));
    /// <inheritdoc/>
    public override Option<TValue> Filter(Func<TValue, bool> predicate) => predicate.Invoke(Value) ? Some(Value) : None();
    /// <inheritdoc/>
    public override Option<TValue> Or(Option<TValue> optb) => this;
    /// <inheritdoc/>
    public override Option<TValue> OrElse(Func<Option<TValue>> f) => this;
    /// <inheritdoc/>
    public override Option<TValue> Xor(Option<TValue> optb) => optb.IsNone ? this : None();
    /// <inheritdoc/>
    public override bool Contains<TOther>(TOther x) => x.Equals(Value);
    /// <inheritdoc/>
    public override Option<(TValue, TValue2)> Zip<TValue2>(Option<TValue2> other) => other is SomeOption<TValue2> some2 ? Option<(TValue, TValue2)>.Some((Value, some2.Value)) : Option<(TValue, TValue2)>.None();
    /// <inheritdoc/>
    public override Option<TNewValue> ZipWith<TValue2, TNewValue>(Option<TValue2> other, Func<TValue, TValue2, TNewValue> f) => other is SomeOption<TValue2> some2 ? Option<TNewValue>.Some(f.Invoke(Value, some2.Value)) : Option<TNewValue>.None();
    /// <inheritdoc/>
    public override void Match(Action<TValue> someAction, Action noneAction) => someAction.Invoke(Value);
    /// <inheritdoc/>
    public override Option<TValue> Clone() => Value is ICloneable cloneableValue ? Some((TValue)cloneableValue.Clone()) : Some(Value);


    /// <summary>
    /// Convert from <see cref="Option.ValueSomeOption{TValue}"/>
    /// </summary>
    /// <param name="valueSome"></param>
    public static implicit operator SomeOption<TValue>(Option.ValueSomeOption<TValue> valueSome) => new SomeOption<TValue>(valueSome.Value);
}