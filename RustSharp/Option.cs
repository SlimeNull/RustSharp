namespace RustSharp
{
    /// <summary>
    /// Represents an optional value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public abstract class Option<TValue> : ICloneable
    {
        public bool IsSome => this is SomeOption<TValue>;
        public bool IsNone => this is NoneOption<TValue>;

        public bool IsSomeAnd(Func<bool> f)
        {
            if (!IsSome)
                return false;
            return f.Invoke();
        }

        public bool IsNoneAnd(Func<bool> f)
        {
            if (!IsNone)
                return false;
            return f.Invoke();
        }

        public TValue Expect(string msg)
        {
            if (this is SomeOption<TValue> some)
                return some.Value;
            throw ExpectException.New(msg);
        }

        public TValue Unwrap()
        {
            if (this is SomeOption<TValue> some)
                return some.Value;
            throw ExpectException.New("Called Option.Unwrap on a None value");
        }

        public TValue UnwrapOr(TValue defaultValue)
        {
            if (this is SomeOption<TValue> some)
                return some.Value;
            return defaultValue;
        }

        public TValue UnwrapOrElse(Func<TValue> f)
        {
            if (this is SomeOption<TValue> some)
                return some.Value;
            return f.Invoke();
        }

        public TValue? UnwrapOrDefault()
        {
            if (this is SomeOption<TValue> some)
                return some.Value;
            return default;
        }

        public Option<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> f)
        {
            if (this is SomeOption<TValue> some)
                return Option<TNewValue>.Some(f.Invoke(some.Value));
            return Option<TNewValue>.None();
        }

        public Option<TValue> Inspect(Action<TValue> action)
        {
            if (this is SomeOption<TValue> some)
                action.Invoke(some.Value);

            return this;
        }

        public TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> f)
        {
            if (this is SomeOption<TValue> some)
                return f.Invoke(some.Value);

            return defaultValue;
        }

        public TNewValue MapOrElse<TNewValue>(Func<TNewValue> defaultValueGetter, Func<TValue, TNewValue> f)
        {
            if (this is SomeOption<TValue> some)
                return f.Invoke(some.Value);

            return defaultValueGetter.Invoke();
        }

        public Result<TValue, TError> OkOr<TError>(TError err)
        {
            if (this is SomeOption<TValue> some)
                return Result<TValue, TError>.Ok(some.Value);

            return Result<TValue, TError>.Err(err);
        }

        public Result<TValue, TError> OkOrElse<TError>(Func<TError> errorGetter)
        {
            if (this is SomeOption<TValue> some)
                return Result<TValue, TError>.Ok(some.Value);

            return Result<TValue, TError>.Err(errorGetter.Invoke());
        }

        public Option<TNewValue> And<TNewValue>(Option<TNewValue> optb)
        {
            if (this is SomeOption<TValue>)
                return optb;

            return Option<TNewValue>.None();
        }    

        public Option<TNewValue> AndThen<TNewValue>(Func<TValue, TNewValue> f)
        {
            if (this is SomeOption<TValue> some)
                return Option<TNewValue>.Some(f.Invoke(some.Value));

            return Option<TNewValue>.None();
        }

        public Option<TValue> Filter(Func<TValue, bool> predicate)
        {
            if (this is SomeOption<TValue> some)
                if (predicate.Invoke(some.Value))
                    return Option<TValue>.Some(some.Value);

            return Option<TValue>.None();
        }

        public Option<TValue> Or(Option<TValue> optb)
        {
            if (this is SomeOption<TValue> some)
                return some;

            return optb;
        }

        public Option<TValue> OrElse(Func<Option<TValue>> f)
        {
            if (this is SomeOption<TValue> some)
                return some;

            return f.Invoke();
        }

        public Option<TValue> Xor(Option<TValue> optb)
        {
            if (this is SomeOption<TValue> some1 && optb is NoneOption<TValue>)
                return some1;
            else if (this is NoneOption<TValue> && optb is SomeOption<TValue> some2)
                return some2;

            return Option<TValue>.None();
        }

        public bool Contains<TOther>(TOther x) where TOther : IEquatable<TValue>
        {
            if (this is SomeOption<TValue> some)
                return x.Equals(some.Value);

            return false;
        }

        public Option<(TValue, TValue2)> Zip<TValue2>(Option<TValue2> other)
        {
            if (this is SomeOption<TValue> a &&
                other is SomeOption<TValue2> b)
                return Option<(TValue, TValue2)>.Some((a.Value, b.Value));

            return Option<(TValue, TValue2)>.None();
        }

        public Option<TNewValue> ZipWith<TValue2, TNewValue>(Option<TValue2> other, Func<TValue, TValue2, TNewValue> f)
        {
            if (this is SomeOption<TValue> a &&
                other is SomeOption<TValue2> b)
                return Option<TNewValue>.Some(f.Invoke(a.Value, b.Value));

            return Option<TNewValue>.None();
        }

        public void Match(Action<TValue> someAction)
        {
            if (this is SomeOption<TValue> some)
                someAction.Invoke(some.Value);
        }

        public Option<TValue> Clone()
        {
            if (this is SomeOption<TValue> some)
            {
                TValue newValue = some.Value;
                if (newValue is ICloneable cloneableNewValue)
                    newValue = (TValue)cloneableNewValue.Clone();

                return Option<TValue>.Some(newValue);
            }

            return Option<TValue>.None();
        }

        public static SomeOption<TValue> Some(TValue value) => new SomeOption<TValue>(value);
        public static NoneOption<TValue> None() => new NoneOption<TValue>();
        object ICloneable.Clone() => throw new NotImplementedException();
    }
}