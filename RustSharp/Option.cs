namespace RustSharp;

/// <summary>
/// Represents an optional value
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class Option<TValue> : ICloneable
    where TValue : notnull
{
    /// <summary>
    /// Returns true if the option is a Some value.
    /// </summary>
    public abstract bool IsSome { get; }

    /// <summary>
    /// Returns true if the option is a None value.
    /// </summary>
    public abstract bool IsNone { get; }

    /// <summary>
    /// Returns true if the option is a Some and the value inside of it matches a predicate.
    /// </summary>
    /// <param name="f">Predicate</param>
    /// <returns></returns>
    public abstract bool IsSomeAnd(Func<TValue, bool> f);

    /// <summary>
    /// Returns the contained <see cref="SomeOption{TValue}"/> value
    /// </summary>
    /// <param name="msg">Custom exception message</param>
    /// <exception cref="ExpectException">The value is a <see cref="NoneOption{TValue}"/></exception>
    /// <returns></returns>
    public abstract TValue Expect(string msg);

    /// <summary>
    /// Returns the contained <see cref="SomeOption{TValue}"/> value
    /// <br />
    /// Because this function may panic, its use is generally discouraged. <br />
    /// Instead, prefer to use pattern matching and handle the <see cref="NoneOption{TValue}"/> <br />
    /// case explicitly, or call <see cref="UnwrapOr(TValue)"/>, <see cref="UnwrapOrElse(Func{TValue})"/>, or <br />
    /// <see cref="UnwrapOrDefault"/>.
    /// </summary>
    /// <exception cref="UnwrapException">The value is a <see cref="NoneOption{TValue}"/></exception>
    /// <returns></returns>
    public abstract TValue Unwrap();

    /// <summary>
    /// Returns the contained <see cref="SomeOption{TValue}"/> value or a provided default. <br />
    /// <br />
    /// Arguments passed to <see cref="UnwrapOr(TValue)"/> are eagerly evaluated; if you are passing <br />
    /// the result of a function call, it is recommended to use <see cref="UnwrapOrElse(Func{TValue})"/>, <br />
    /// which is lazily evaluated. <br />
    /// </summary>
    /// <param name="defaultValue">Default value</param>
    /// <returns></returns>
    public abstract TValue UnwrapOr(TValue defaultValue);

    /// <summary>
    /// Returns the contained <see cref="SomeOption{TValue}"/> value or computes it from a closure.
    /// </summary>
    /// <param name="f">Closure to be computed</param>
    /// <returns></returns>
    public abstract TValue UnwrapOrElse(Func<TValue> f);

    /// <summary>
    /// Returns the contained <see cref="SomeOption{TValue}"/> value or a default. <br />
    /// <br />
    /// If <see cref="SomeOption{TValue}"/>, returns the contained value, <br />
    /// otherwise if <see cref="NoneOption{TValue}"/>, returns the default value for that type.
    /// </summary>
    /// <returns></returns>
    public abstract TValue? UnwrapOrDefault();

    /// <summary>
    /// Maps an Option&lt;T&gt; to Option&lt;U&gt; by applying a function to a contained value (if <see cref="SomeOption{TValue}"/>) or returns None (if <see cref="NoneOption{TValue}"/>).
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="f"></param>
    /// <returns></returns>
    public abstract Option<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> f)
        where TNewValue : notnull;

    /// <summary>
    /// Calls the provided closure with a reference to the contained value (if <see cref="SomeOption{TValue}"/>).
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public abstract Option<TValue> Inspect(Action<TValue> action);

    /// <summary>
    /// Returns the provided default result (if none), <br />
    /// or applies a function to the contained value(if any). <br />
    /// <br />
    /// Arguments passed to <see cref="MapOr{TNewValue}(TNewValue, Func{TValue, TNewValue})"/> are eagerly evaluated; if you are passing <br />
    /// the result of a function call, it is recommended to use <see cref="MapOrElse{TNewValue}(Func{TNewValue}, Func{TValue, TNewValue})"/>, <br />
    /// which is lazily evaluated.
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public abstract TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> f);

    /// <summary>
    /// Computes a default function result (if none), or <br />
    /// applies a different function to the contained value(if any).
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="defaultValueGetter"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public abstract TNewValue MapOrElse<TNewValue>(Func<TNewValue> defaultValueGetter, Func<TValue, TNewValue> f);

    /// <summary>
    /// Transforms the Option&lt;T&gt; into a Result&lt;T, E&gt;, mapping Some(v) to <br />
    /// Ok(v) and None to Err(err). <br />
    /// <br />
    /// Arguments passed to ok_or are eagerly evaluated; if you are passing the <br />
    /// result of a function call, it is recommended to use <see cref="OkOrElse{TError}(Func{TError})"/>, which is <br />
    /// lazily evaluated.
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="err"></param>
    /// <returns></returns>
    public abstract Result<TValue, TError> OkOr<TError>(TError err)
        where TError : notnull;

    /// <summary>
    /// Transforms the Option&lt;T&gt; into a Result&lt;T, E&gt;, mapping Some(v) to <br />
    /// Ok(v) and None to Err(err()).
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="errorGetter"></param>
    /// <returns></returns>
    public abstract Result<TValue, TError> OkOrElse<TError>(Func<TError> errorGetter)
        where TError : notnull;

    /// <summary>
    /// Returns <see cref="NoneOption{TValue}"/> if the option is <see cref="NoneOption{TValue}"/>, otherwise returns `optb`. <br />
    /// <br />
    /// Arguments passed to <see cref="And{TNewValue}(Option{TNewValue})"/> are eagerly evaluated; if you are passing the <br />
    /// result of a function call, it is recommended to use <see cref="AndThen{TNewValue}(Func{TValue, TNewValue})"/>, which is <br />
    /// lazily evaluated.
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="optb"></param>
    /// <returns></returns>
    public abstract Option<TNewValue> And<TNewValue>(Option<TNewValue> optb) 
        where TNewValue : notnull;

    /// <summary>
    /// Returns <see cref="NoneOption{TValue}"/> if the option is <see cref="NoneOption{TValue}"/>, otherwise calls <paramref name="f"/> with the <br />
    /// wrapped value and returns the result. <br />
    /// <br />
    /// Some languages call this operation flatmap.
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="f"></param>
    /// <returns></returns>
    public abstract Option<TNewValue> AndThen<TNewValue>(Func<TValue, TNewValue> f) 
        where TNewValue : notnull;

    /// <summary>
    /// Returns <see cref="NoneOption{TValue}"/> if the option is <see cref="NoneOption{TValue}"/>, otherwise calls <paramref name="predicate"/> <br />
    /// with the wrapped value and returns: <br />
    /// <br />
    /// - Some(t) if <paramref name="predicate"/> returns true (where t is the wrapped <br />
    ///   value), and <br />
    /// - None if <paramref name="predicate"/> returns false. <br />
    /// <br />
    /// This function works similar to [`Iterator::filter()`]. You can imagine <br />
    /// the Option&lt;T&gt; being an iterator over one or zero elements. `filter()` <br />
    /// lets you decide which elements to keep.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public abstract Option<TValue> Filter(Func<TValue, bool> predicate);

    /// <summary>
    /// Returns the option if it contains a value, otherwise returns `optb`. <br />
    /// <br />
    /// Arguments passed to <see cref="Or(Option{TValue})"/> are eagerly evaluated; if you are passing the <br />
    /// result of a function call, it is recommended to use <see cref="OrElse(Func{Option{TValue}})"/>, which is <br />
    /// lazily evaluated.
    /// </summary>
    /// <param name="optb"></param>
    /// <returns></returns>
    public abstract Option<TValue> Or(Option<TValue> optb);

    /// <summary>
    /// Returns the option if it contains a value, otherwise calls <paramref name="f"/> and <br />
    /// returns the result.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public abstract Option<TValue> OrElse(Func<Option<TValue>> f);

    /// <summary>
    /// Returns <see cref="SomeOption{TValue}"/> if exactly one of self, `optb` is <see cref="SomeOption{TValue}"/>, otherwise returns <see cref="NoneOption{TValue}"/>.
    /// </summary>
    /// <param name="optb"></param>
    /// <returns></returns>
    public abstract Option<TValue> Xor(Option<TValue> optb);

    /// <summary>
    /// Returns true if specified value equals contained value
    /// </summary>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public abstract bool Contains<TOther>(TOther x) where TOther : IEquatable<TValue>;

    /// <summary>
    /// Zips self with another Option. <br />
    /// <br />
    /// If self is Some(s) and <paramref name="other"/> is Some(o), this method returns Some((s, o)). <br />
    /// Otherwise, None is returned.
    /// </summary>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="other"></param>
    /// <returns></returns>
    public abstract Option<(TValue, TValue2)> Zip<TValue2>(Option<TValue2> other)
        where TValue2 : notnull;

    /// <summary>
    /// Zips self and another Option with function <paramref name="f"/>. <br />
    /// <br />
    /// If self is Some(s) and <paramref name="other"/> is Some(o), this method returns Some(f(s, o)). <br />
    /// Otherwise, None is returned.
    /// </summary>
    /// <typeparam name="TValue2"></typeparam>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="other"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    public abstract Option<TNewValue> ZipWith<TValue2, TNewValue>(Option<TValue2> other, Func<TValue, TValue2, TNewValue> f)
        where TValue2 : notnull
        where TNewValue : notnull;

    /// <summary>
    /// Perform the corresponding operation based on the value of the current Option
    /// </summary>
    /// <param name="someAction"></param>
    /// <param name="noneAction"></param>
    public abstract void Match(Action<TValue> someAction, Action noneAction);

    /// <summary>
    /// Clone the current Option
    /// </summary>
    /// <returns></returns>
    public abstract Option<TValue> Clone();


    /// <summary>
    /// Create an <see cref="Option{TValue}"/> from specified value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Option<TValue> Create(TValue? value) => value is null ? new NoneOption<TValue>() : new SomeOption<TValue>(value);

    /// <summary>
    /// Create a <see cref="SomeOption{TValue}"/> from specified value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SomeOption<TValue> Some(TValue value) => new SomeOption<TValue>(value ?? throw new ArgumentNullException());

    /// <summary>
    /// Create a <see cref="NoneOption{TValue}"/>
    /// </summary>
    /// <returns></returns>
    public static NoneOption<TValue> None() => new NoneOption<TValue>();

    /// <summary>
    /// Convert from <see cref="Option.ValueSomeOption{TValue}"/>
    /// </summary>
    /// <param name="valueSome"></param>
    
    public static implicit operator Option<TValue>(Option.ValueSomeOption<TValue> valueSome) => new SomeOption<TValue>(valueSome.Value);

    /// <summary>
    /// Convert from <see cref="Option.ValueNoneOption"/>
    /// </summary>
    /// <param name="valueNone"></param>

    public static implicit operator Option<TValue>(Option.ValueNoneOption valueNone) => new NoneOption<TValue>();


    object ICloneable.Clone() => Clone();
}

/// <summary>
/// Utils for <see cref="Option{TValue}"/>
/// </summary>
public static class Option
{
    /// <summary>
    /// Create an <see cref="Option{TValue}"/> from specified value
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Option<TValue> Create<TValue>(TValue? value)
        where TValue : notnull
        => value is null ? new NoneOption<TValue>() : new SomeOption<TValue>(value);

    /// <summary>
    /// Create a <see cref="ValueSomeOption{TValue}"/> for <see cref="Option{TValue}"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ValueSomeOption<TValue> Some<TValue>(TValue value)
        where TValue : notnull
        => new ValueSomeOption<TValue>(value ?? throw new ArgumentNullException());

    /// <summary>
    /// Create a <see cref="ValueNoneOption"/> for any <see cref="Option{TValue}"/>.
    /// </summary>
    /// <returns></returns>
    public static ValueNoneOption None() => new ValueNoneOption();

    /// <summary>
    /// Struct that can be implicitly converted to <see cref="Option{TValue}"/>.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="Value"></param>
    public record struct ValueSomeOption<TValue>(TValue Value)
        where TValue : notnull;

    /// <summary>
    /// Struct that can be implicitly converted to any <see cref="Option{TValue}"/>.
    /// </summary>
    public record struct ValueNoneOption;
}