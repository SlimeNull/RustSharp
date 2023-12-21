namespace RustSharp;

/// <summary>
/// No value for <see cref="SomeOption{TValue}"/>
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class NoneOption<TValue> : Option<TValue>
{
    internal NoneOption()
    {

    }

    /// <inheritdoc/>
    public override bool IsSome => false;

    /// <inheritdoc/>
    public override bool IsNone => true;

    /// <inheritdoc/>
    public override bool IsSomeAnd(Func<TValue, bool> f) => false;
    /// <inheritdoc/>
    public override TValue Expect(string msg) => throw new ExpectException(msg);
    /// <inheritdoc/>
    public override TValue Unwrap() => throw new UnwrapException("Called Option.Unwrap on a None value");
    /// <inheritdoc/>
    public override TValue UnwrapOr(TValue defaultValue) => defaultValue;
    /// <inheritdoc/>
    public override TValue UnwrapOrElse(Func<TValue> f) => f.Invoke();
    /// <inheritdoc/>
    public override TValue? UnwrapOrDefault() => default;
    /// <inheritdoc/>
    public override Option<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> f) => Option<TNewValue>.None();
    /// <inheritdoc/>
    public override Option<TValue> Inspect(Action<TValue> action) => this;
    /// <inheritdoc/>
    public override TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> f) => defaultValue;
    /// <inheritdoc/>
    public override TNewValue MapOrElse<TNewValue>(Func<TNewValue> defaultValueGetter, Func<TValue, TNewValue> f) => defaultValueGetter.Invoke();
    /// <inheritdoc/>
    public override Result<TValue, TError> OkOr<TError>(TError err) => Result<TValue, TError>.Err(err);
    /// <inheritdoc/>
    public override Result<TValue, TError> OkOrElse<TError>(Func<TError> errorGetter) => Result<TValue, TError>.Err(errorGetter.Invoke());
    /// <inheritdoc/>
    public override Option<TNewValue> And<TNewValue>(Option<TNewValue> optb) => Option<TNewValue>.None();
    /// <inheritdoc/>
    public override Option<TNewValue> AndThen<TNewValue>(Func<TValue, TNewValue> f) => Option<TNewValue>.None();
    /// <inheritdoc/>
    public override Option<TValue> Filter(Func<TValue, bool> predicate) => None();
    /// <inheritdoc/>
    public override Option<TValue> Or(Option<TValue> optb) => optb;
    /// <inheritdoc/>
    public override Option<TValue> OrElse(Func<Option<TValue>> f) => f.Invoke();
    /// <inheritdoc/>
    public override Option<TValue> Xor(Option<TValue> optb) => optb.IsSome ? optb : None();
    /// <inheritdoc/>
    public override bool Contains<TOther>(TOther x) => false;
    /// <inheritdoc/>
    public override Option<(TValue, TValue2)> Zip<TValue2>(Option<TValue2> other) => Option<(TValue, TValue2)>.None();
    /// <inheritdoc/>
    public override Option<TNewValue> ZipWith<TValue2, TNewValue>(Option<TValue2> other, Func<TValue, TValue2, TNewValue> f) => Option<TNewValue>.None();
    /// <inheritdoc/>
    public override void Match(Action<TValue> someAction, Action noneAction) => noneAction.Invoke();
    /// <inheritdoc/>
    public override Option<TValue> Clone() => None();


    /// <summary>
    /// Convert from <see cref="Option.ValueNoneOption"/>
    /// </summary>
    /// <param name="valueNone"></param>
    public static implicit operator NoneOption<TValue>(Option.ValueNoneOption valueNone) => new NoneOption<TValue>();
}