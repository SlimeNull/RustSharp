namespace RustSharp
{
    /// <summary>
    /// Represents an optional value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public abstract class Option<TValue> : ICloneable
    {
        public abstract bool IsSome { get; }
        public abstract bool IsNone { get; }

        public abstract bool IsSomeAnd(Func<TValue, bool> f);

        public abstract TValue Expect(string msg);

        public abstract TValue Unwrap();

        public abstract TValue UnwrapOr(TValue defaultValue);

        public abstract TValue UnwrapOrElse(Func<TValue> f);

        public abstract TValue? UnwrapOrDefault();

        public abstract Option<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> f);

        public abstract Option<TValue> Inspect(Action<TValue> action);

        public abstract TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> f);

        public abstract TNewValue MapOrElse<TNewValue>(Func<TNewValue> defaultValueGetter, Func<TValue, TNewValue> f);

        public abstract Result<TValue, TError> OkOr<TError>(TError err);

        public abstract Result<TValue, TError> OkOrElse<TError>(Func<TError> errorGetter);

        public abstract Option<TNewValue> And<TNewValue>(Option<TNewValue> optb);

        public abstract Option<TNewValue> AndThen<TNewValue>(Func<TValue, TNewValue> f);

        public abstract Option<TValue> Filter(Func<TValue, bool> predicate);

        public abstract Option<TValue> Or(Option<TValue> optb);

        public abstract Option<TValue> OrElse(Func<Option<TValue>> f);

        public abstract Option<TValue> Xor(Option<TValue> optb);

        public abstract bool Contains<TOther>(TOther x) where TOther : IEquatable<TValue>;

        public abstract Option<(TValue, TValue2)> Zip<TValue2>(Option<TValue2> other);

        public abstract Option<TNewValue> ZipWith<TValue2, TNewValue>(Option<TValue2> other, Func<TValue, TValue2, TNewValue> f);

        public abstract void Match(Action<TValue> someAction, Action noneAction);

        public abstract Option<TValue> Clone();

        public static SomeOption<TValue> Some(TValue value) => new SomeOption<TValue>(value);
        public static NoneOption<TValue> None() => new NoneOption<TValue>();

        
        public static implicit operator Option<TValue>(Option.ValueSomeOption<TValue> valueSome) => new SomeOption<TValue>(valueSome.Value);

        public static implicit operator Option<TValue>(Option.ValueNoneOption valueNone) => new NoneOption<TValue>();


        object ICloneable.Clone() => Clone();
    }

    public static class Option
    {
        public static ValueSomeOption<TValue> Some<TValue>(TValue value) => new ValueSomeOption<TValue>(value);
        public static ValueNoneOption None() => new ValueNoneOption();

        public record struct ValueSomeOption<TValue>(TValue Value);
        public record struct ValueNoneOption;
    }
}