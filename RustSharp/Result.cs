namespace RustSharp;

/// <summary>
/// <see cref="Result{TValue, TError}"/> is a type that represents either success (<see cref="OkResult{TValue, TError}"/>) or failure (<see cref="ErrResult{TValue, TError}"/>).
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TError"></typeparam>
public abstract class Result<TValue, TError> : ICloneable
{
    /// <summary>
    /// Returns true if the result is <see cref="OkResult{TValue, TError}"/>.
    /// </summary>
    public abstract bool IsOk { get; }

    /// <summary>
    /// Returns true if the result is <see cref="ErrResult{TValue, TError}"/>.
    /// </summary>
    public abstract bool IsErr { get; }

    /// <summary>
    /// Returns true if the result is <see cref="OkResult{TValue, TError}"/> and the value inside of it matches a predicate.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public abstract bool IsOkAnd(Func<TValue, bool> f);

    /// <summary>
    /// Returns true if the result is <see cref="ErrResult{TValue, TError}"/> and the value inside of it matches a predicate.
    /// </summary>
    /// <param name="f"></param>
    /// <returns></returns>
    public abstract bool IsErrAnd(Func<TError, bool> f);

    /// <summary>
    /// Converts from Result&lt;TValue, TError&gt; to <see cref="Option{TValue}"/>. <br />
    /// <br />
    /// Converts self into an <see cref="Option{TValue}"/>, consuming self, <br />
    /// and discarding the error, if any.
    /// </summary>
    /// <returns></returns>
    public abstract Option<TValue> Ok();

    /// <summary>
    /// Converts from Result&lt;TValue, TError&gt; to Option&lt;TError&gt;. <br />
    /// <br />
    /// Converts self into an Option&lt;TError&gt;, consuming self, <br />
    /// and discarding the success value, if any.
    /// </summary>
    /// <returns></returns>
    public abstract Option<TError> Err();

    /// <summary>
    /// Maps a Result&lt;TValue, TError&gt; to Result&lt;TNewValue, TError&gt; by applying a function to a <br />
    /// <br />
    /// contained <see cref="OkResult{TValue, TError}"/> value, leaving an <see cref="ErrResult{TValue, TError}"/> value untouched. <br />
    /// This function can be used to compose the results of two functions.
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="mapper"></param>
    /// <returns></returns>
    public abstract Result<TNewValue, TError> Map<TNewValue>(Func<TValue, TNewValue> mapper);

    /// <summary>
    /// Returns the provided default (if <see cref="ErrResult{TValue, TError}"/>), or <br />
    /// applies a function to the contained value(if <see cref="OkResult{TValue, TError}"/>). <br />
    /// <br />
    /// Arguments passed to `map_or` are eagerly evaluated; if you are passing <br />
    /// the result of a function call, it is recommended to use[`map_or_else`], <br />
    /// which is lazily evaluated.
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <param name="mapper"></param>
    /// <returns></returns>
    public abstract TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> mapper);

    /// <summary>
    /// Maps a Result&lt;TValue, TError&gt; to `TNewValue` by applying fallback function `default` to <br />
    /// a contained <see cref="ErrResult{TValue, TError}"/> value, or function `f` to a contained<see cref="OkResult{TValue, TError}"/> value. <br />
    /// <br />
    /// This function can be used to unpack a successful result <br />
    /// while handling an error.
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="errorMapper"></param>
    /// <param name="mapper"></param>
    /// <returns></returns>
    public abstract TNewValue MapOrElse<TNewValue>(Func<TError, TNewValue> errorMapper, Func<TValue, TNewValue> mapper);

    /// <summary>
    /// Maps a Result&lt;TValue, TError&gt; to Result&lt;T, F&gt; by applying a function to a <br />
    /// contained <see cref="ErrResult{TValue, TError}"/> value, leaving an <see cref="OkResult{TValue, TError}"/> value untouched. <br />
    /// <br />
    /// This function can be used to pass through a successful result while handling <br />
    /// an error.
    /// </summary>
    /// <typeparam name="TNewError"></typeparam>
    /// <param name="mapper"></param>
    /// <returns></returns>
    public abstract Result<TValue, TNewError> MapErr<TNewError>(Func<TError, TNewError> mapper);

    /// <summary>
    /// Calls the provided closure with a reference to the contained value (if <see cref="OkResult{TValue, TError}"/>).
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public abstract Result<TValue, TError> Inspect(Action<TValue> action);

    /// <summary>
    /// Calls the provided closure with a reference to the contained error (if <see cref="ErrResult{TValue, TError}"/>).
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public abstract Result<TValue, TError> InspectErr(Action<TError> action);

    /// <summary>
    /// Returns the contained <see cref="OkResult{TValue, TError}"/> value, consuming the self value. <br />
    /// <br />
    /// Because this function may panic, its use is generally discouraged. <br />
    /// Instead, prefer to use pattern matching and handle the <see cref="ErrResult{TValue, TError}"/> <br />
    /// case explicitly, or call [`unwrap_or`], [`unwrap_or_else`], or <br />
    /// [`unwrap_or_default`].
    /// </summary>
    /// <param name="msg"></param>
    /// <exception cref="ExpectException">The value is an <see cref="ErrResult{TValue, TError}"/></exception>
    /// <returns></returns>
    public abstract TValue Expect(string msg);

    /// <summary>
    /// Returns the contained <see cref="OkResult{TValue, TError}"/> value, consuming the self value. <br />
    /// <br />
    /// Because this function may panic, its use is generally discouraged. <br />
    /// Instead, prefer to use pattern matching and handle the <see cref="ErrResult{TValue, TError}"/> <br />
    /// case explicitly, or call [`unwrap_or`], [`unwrap_or_else`], or <br />
    /// [`unwrap_or_default`].
    /// </summary>
    /// <exception cref="UnwrapException">The value is an <see cref="ErrResult{TValue, TError}"/></exception>
    /// <returns></returns>
    public abstract TValue Unwrap();

    /// <summary>
    /// Returns the contained <see cref="OkResult{TValue, TError}"/> value or a default <br />
    /// <br />
    /// If <see cref="OkResult{TValue, TError}"/>, returns the contained <br />
    /// value, otherwise if <see cref="ErrResult{TValue, TError}"/>, returns the default value for that <br />
    /// type.
    /// </summary>
    /// <returns></returns>
    public abstract TValue? UnwrapOrDefault();

    /// <summary>
    /// Returns the contained <see cref="ErrResult{TValue, TError}"/> value
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public abstract TError ExpectErr(string msg);

    /// <summary>
    /// Returns the contained <see cref="ErrResult{TValue, TError}"/> value, consuming the self value.
    /// </summary>
    /// <exception cref="UnwrapException">The value is an <see cref="OkResult{TValue, TError}"/></exception>
    /// <returns></returns>
    public abstract TError UnwrapErr();

    /// <summary>
    /// Returns `res` if the result is <see cref="OkResult{TValue, TError}"/>, otherwise returns the <see cref="ErrResult{TValue, TError}"/> value of self. <br />
    /// <br />
    /// Arguments passed to `and` are eagerly evaluated; if you are passing the <br />
    /// result of a function call, it is recommended to use[`and_then`], which is <br />
    /// lazily evaluated.
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="res"></param>
    /// <returns></returns>
    public abstract Result<TNewValue, TError> And<TNewValue>(Result<TNewValue, TError> res);

    /// <summary>
    /// Calls `op` if the result is <see cref="OkResult{TValue, TError}"/>, otherwise returns the <see cref="ErrResult{TValue, TError}"/> value of self. <br />
    /// <br />
    /// This function can be used for control flow based on `Result` values.
    /// </summary>
    /// <typeparam name="TNewValue"></typeparam>
    /// <param name="operation"></param>
    /// <returns></returns>
    public abstract Result<TNewValue, TError> AndThen<TNewValue>(Func<TValue, Result<TNewValue, TError>> operation);

    /// <summary>
    /// Returns `res` if the result is <see cref="ErrResult{TValue, TError}"/>, otherwise returns the <see cref="OkResult{TValue, TError}"/> value of self. <br />
    /// <br />
    /// Arguments passed to `or` are eagerly evaluated; if you are passing the <br />
    /// result of a function call, it is recommended to use[`or_else`], which is <br />
    /// lazily evaluated.
    /// </summary>
    /// <param name="res"></param>
    /// <returns></returns>
    public abstract Result<TValue, TError> Or(Result<TValue, TError> res);

    /// <summary>
    /// Calls `op` if the result is <see cref="ErrResult{TValue, TError}"/>, otherwise returns the <see cref="OkResult{TValue, TError}"/> value of self. <br />
    /// <br />
    /// This function can be used for control flow based on result values.
    /// </summary>
    /// <typeparam name="TNewError"></typeparam>
    /// <param name="operation"></param>
    /// <returns></returns>
    public abstract Result<TValue, TNewError> OrElse<TNewError>(Func<TError, Result<TValue, TNewError>> operation);

    /// <summary>
    /// Returns the contained <see cref="OkResult{TValue, TError}"/> value or a provided default. <br />
    /// <br />
    /// Arguments passed to `unwrap_or` are eagerly evaluated; if you are passing <br />
    /// the result of a function call, it is recommended to use [`unwrap_or_else`], <br />
    /// which is lazily evaluated.
    /// </summary>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public abstract TValue UnwrapOr(TValue defaultValue);

    /// <summary>
    /// Returns the contained <see cref="OkResult{TValue, TError}"/> value or computes it from a closure.
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public abstract TValue UnwrapOrElse(Func<TError, TValue> operation);

    /// <summary>
    /// Returns true if specified value equals contained ok value
    /// </summary>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public abstract bool Contains<TOther>(TOther x) where TOther : IEquatable<TValue>;

    /// <summary>
    /// Returns true if specified value equals contained err value
    /// </summary>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="f"></param>
    /// <returns></returns>
    public abstract bool ContainsErr<TOther>(TOther f) where TOther : IEquatable<TError>;

    /// <summary>
    /// Perform the corresponding operation based on the value of the current Option
    /// </summary>
    /// <param name="okAction"></param>
    /// <param name="errAction"></param>
    public abstract void Match(Action<TValue> okAction, Action<TError> errAction);

    /// <summary>
    /// Clone the current Result
    /// </summary>
    /// <returns></returns>
    public abstract Result<TValue, TError> Clone();

    /// <summary>
    /// Converts from Result&lt;TValue, TError&gt; to <see cref="Option{TValue}"/>. <br />
    /// <br />
    /// Converts self into an <see cref="Option{TValue}"/>, consuming self, <br />
    /// and discarding the error, if any.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static OkResult<TValue, TError> Ok(TValue value) => new OkResult<TValue, TError>(value);

    /// <summary>
    /// Converts from Result&lt;TValue, TError&gt; to Option&lt;TError&gt;. <br />
    /// <br />
    /// Converts self into an Option&lt;TError&gt;, consuming self, <br />
    /// and discarding the success value, if any.
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static ErrResult<TValue, TError> Err(TError error) => new ErrResult<TValue, TError>(error);

    /// <summary>
    /// Convert from <see cref="Result.ValueOkResult{TValue}"/>
    /// </summary>
    /// <param name="valueOk"></param>
    public static implicit operator Result<TValue, TError>(Result.ValueOkResult<TValue> valueOk) => new OkResult<TValue, TError>(valueOk.Value);

    /// <summary>
    /// Convert from <see cref="Result.ValueErrResult{TValue}"/>
    /// </summary>
    /// <param name="valueErr"></param>
    public static implicit operator Result<TValue, TError>(Result.ValueErrResult<TError> valueErr) => new ErrResult<TValue, TError>(valueErr.Value);

    object ICloneable.Clone() => Clone();
}

/// <summary>
/// Utils for <see cref="Result{TValue, TError}"/>
/// </summary>
public static class Result
{
    /// <summary>
    /// Create a <see cref="ValueOkResult{TError}"/> for any <see cref="Result{TValue, TError}"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ValueOkResult<TValue> Ok<TValue>(TValue value) => new ValueOkResult<TValue>(value);

    /// <summary>
    /// Create a <see cref="ValueErrResult{TError}"/> for any <see cref="Result{TValue, TError}"/>
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="error"></param>
    /// <returns></returns>
    public static ValueErrResult<TError> Err<TError>(TError error) => new ValueErrResult<TError>(error);

    /// <summary>
    /// Struct that can be implicitly converted to any <see cref="Result{TValue, TError}"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="Value"></param>
    public record struct ValueOkResult<TValue>(TValue Value);

    /// <summary>
    /// Struct that can be implicitly converted to any <see cref="Result{TValue, TError}"/>
    /// </summary>
    /// <typeparam name="TError"></typeparam>
    /// <param name="Value"></param>
    public record struct ValueErrResult<TError>(TError Value);
}