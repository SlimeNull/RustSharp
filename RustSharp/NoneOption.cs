namespace RustSharp
{
    /// <summary>
    /// No value.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class NoneOption<TValue> : Option<TValue>
    {
        internal NoneOption()
        {

        }

        public override bool IsSome => false;

        public override bool IsNone => true;

        public override bool IsSomeAnd(Func<TValue, bool> f) => false;
        public override TValue Expect(string msg) => throw ExpectException.New(msg);
        public override TValue Unwrap() => throw ExpectException.New("Called Option.Unwrap on a None value");
        public override TValue UnwrapOr(TValue defaultValue) => defaultValue;
        public override TValue UnwrapOrElse(Func<TValue> f) => f.Invoke();
        public override TValue? UnwrapOrDefault() => default;
        public override Option<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> f) => Option<TNewValue>.None();
        public override Option<TValue> Inspect(Action<TValue> action) => this;
        public override TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> f) => defaultValue;
        public override TNewValue MapOrElse<TNewValue>(Func<TNewValue> defaultValueGetter, Func<TValue, TNewValue> f) => defaultValueGetter.Invoke();
        public override Result<TValue, TError> OkOr<TError>(TError err) => Result<TValue, TError>.Err(err);
        public override Result<TValue, TError> OkOrElse<TError>(Func<TError> errorGetter) => Result<TValue, TError>.Err(errorGetter.Invoke());
        public override Option<TNewValue> And<TNewValue>(Option<TNewValue> optb) => Option<TNewValue>.None();
        public override Option<TNewValue> AndThen<TNewValue>(Func<TValue, TNewValue> f) => Option<TNewValue>.None();
        public override Option<TValue> Filter(Func<TValue, bool> predicate) => None();
        public override Option<TValue> Or(Option<TValue> optb) => optb;
        public override Option<TValue> OrElse(Func<Option<TValue>> f) => f.Invoke();
        public override Option<TValue> Xor(Option<TValue> optb) => optb.IsSome ? optb : None();
        public override bool Contains<TOther>(TOther x) => false;
        public override Option<(TValue, TValue2)> Zip<TValue2>(Option<TValue2> other) => Option<(TValue, TValue2)>.None();
        public override Option<TNewValue> ZipWith<TValue2, TNewValue>(Option<TValue2> other, Func<TValue, TValue2, TNewValue> f) => Option<TNewValue>.None();
        public override void Match(Action<TValue> someAction, Action noneAction) => noneAction.Invoke();
        public override Option<TValue> Clone() => None();


        public static implicit operator NoneOption<TValue>(Option.ValueNoneOption valueNone) => new NoneOption<TValue>();
    }
}