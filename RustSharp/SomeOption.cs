using System.Runtime.CompilerServices;

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

        public override bool IsSome => true;

        public override bool IsNone => false;

        internal SomeOption(TValue value)
        {
            Value = value;
        }

        public override bool IsSomeAnd(Func<TValue, bool> f) => f.Invoke(Value);
        public override TValue Expect(string msg) => Value;
        public override TValue Unwrap() => Value;
        public override TValue UnwrapOr(TValue defaultValue) => Value;
        public override TValue UnwrapOrElse(Func<TValue> f) => Value;
        public override TValue? UnwrapOrDefault() => Value;
        public override Option<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> f) => Option<TNewValue>.Some(f.Invoke(Value));
        public override Option<TValue> Inspect(Action<TValue> action)
        {
            action.Invoke(Value);

            return this;
        }

        public override TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> f) => f.Invoke(Value);
        public override TNewValue MapOrElse<TNewValue>(Func<TNewValue> defaultValueGetter, Func<TValue, TNewValue> f) => f.Invoke(Value);
        public override Result<TValue, TError> OkOr<TError>(TError err) => Result<TValue, TError>.Ok(Value);
        public override Result<TValue, TError> OkOrElse<TError>(Func<TError> errorGetter) => Result<TValue, TError>.Ok(Value);
        public override Option<TNewValue> And<TNewValue>(Option<TNewValue> optb) => optb;
        public override Option<TNewValue> AndThen<TNewValue>(Func<TValue, TNewValue> f) => Option<TNewValue>.Some(f.Invoke(Value));
        public override Option<TValue> Filter(Func<TValue, bool> predicate) => predicate.Invoke(Value) ? Some(Value) : None();
        public override Option<TValue> Or(Option<TValue> optb) => this;
        public override Option<TValue> OrElse(Func<Option<TValue>> f) => this;
        public override Option<TValue> Xor(Option<TValue> optb) => optb.IsNone ? this : None();
        public override bool Contains<TOther>(TOther x) => x.Equals(Value);
        public override Option<(TValue, TValue2)> Zip<TValue2>(Option<TValue2> other) => other is SomeOption<TValue2> some2 ? Option<(TValue, TValue2)>.Some((Value, some2.Value)) : Option<(TValue, TValue2)>.None();
        public override Option<TNewValue> ZipWith<TValue2, TNewValue>(Option<TValue2> other, Func<TValue, TValue2, TNewValue> f) => other is SomeOption<TValue2> some2 ? Option<TNewValue>.Some(f.Invoke(Value, some2.Value)) : Option<TNewValue>.None();
        public override void Match(Action<TValue> someAction, Action noneAction) => someAction.Invoke(Value);
        public override Option<TValue> Clone() => Value is ICloneable cloneableValue ? Some((TValue)cloneableValue.Clone()) : Some(Value);


        public static implicit operator SomeOption<TValue>(Option.ValueSomeOption<TValue> valueSome) => new SomeOption<TValue>(valueSome.Value);
    }
}