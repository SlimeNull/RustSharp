namespace RustSharp;

/// <summary>
/// Extensions for <see cref="Option{TValue}"/>
/// </summary>
public static class OptionExtensions
{
    /// <summary>
    /// Unzips an option containing a tuple of two options. <br />
    /// <br />
    /// If `self` is `Some((a, b))` this method returns `(Some(a), Some(b))`. <br />
    /// Otherwise, `(None, None)` is returned. 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TValue2"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static (Option<TValue> Option1, Option<TValue2> Option2) Unzip<TValue, TValue2>(this Option<(TValue, TValue2)> self)
    {
        if (self is SomeOption<(TValue, TValue2)> some)
            return (Option<TValue>.Some(some.Value.Item1), Option<TValue2>.Some(some.Value.Item2));
        else
            return (Option<TValue>.None(), Option<TValue2>.None());
    }

    /// <summary>
    /// Transposes an Option of a Result into a Result of an Option. <br />
    /// <br />
    /// None will be mapped to　Ok(None). <br />
    /// Some(Ok(_)) and　Some(Err(_)) will be mapped to <br />
    /// Ok(Some(_)) and　Err(_).
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TError"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Result<Option<TValue>, TError> Transpose<TValue, TError>(this Option<Result<TValue, TError>> self)
    {
        if (self is SomeOption<Result<TValue, TError>> some)
        {
            if (some.Value is OkResult<TValue, TError> ok)
                return Result<Option<TValue>, TError>.Ok(Option<TValue>.Some(ok.Value));
            else if (some.Value is ErrResult<TValue, TError> err)
                return Result<Option<TValue>, TError>.Err(err.Value);
            throw new InvalidOperationException();
        }
        else
        {
            return Result<Option<TValue>, TError>.Ok(Option<TValue>.None());
        }
    }
}