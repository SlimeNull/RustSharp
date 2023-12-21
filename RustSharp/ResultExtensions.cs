namespace RustSharp;

/// <summary>
/// Extensions for <see cref="Result{TValue, TError}"/>
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts from Result&lt;Result&lt;TValue, TError&gt;, TError&gt; to Result&lt;TValue, TError&gt;
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TError"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    public static Result<TValue, TError> Flatten<TValue, TError>(this Result<Result<TValue, TError>, TError> self)
    {
        return self.AndThen(x => x);
    }

    /// <summary>
    /// Transposes a Result of an Option into an Option of a Result. <br />
    /// <br />
    /// Ok(None) will be mapped to None. <br />
    /// Ok(Some(_)) and Err(_) will be mapped to Some(Ok(_)) and Some(Err(_)). <br />
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TError"></typeparam>
    /// <param name="self"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Option<Result<TValue, TError>> Transpose<TValue, TError>(this Result<Option<TValue>, TError> self)
    {
        return self switch
        {
            OkResult<Option<TValue>, TError> ok => ok.Value switch
            {
                SomeOption<TValue> some => Option<Result<TValue, TError>>.Some(Result<TValue, TError>.Ok(some.Value)),
                NoneOption<TValue> none => Option<Result<TValue, TError>>.None(),
                _ => throw new InvalidOperationException()
            },
            ErrResult<Option<TValue>, TError> err => Option<Result<TValue, TError>>.Some(Result<TValue, TError>.Err(err.Value)),
            _ => throw new InvalidOperationException(),
        };
    }
}