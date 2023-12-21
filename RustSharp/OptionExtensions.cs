namespace RustSharp
{
    public static class OptionExtensions
    {
        public static (Option<TValue> Option1, Option<TValue2> Option2) Unzip<TValue, TValue2>(this Option<(TValue, TValue2)> self)
        {
            if (self is SomeOption<(TValue, TValue2)> some)
                return (Option<TValue>.Some(some.Value.Item1), Option<TValue2>.Some(some.Value.Item2));
            else
                return (Option<TValue>.None(), Option<TValue2>.None());
        }

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
}