namespace RustSharp
{
    public static class ResultExtensions
    {
        public static Result<TValue, TError> Flatten<TValue, TError>(this Result<Result<TValue, TError>, TError> self)
        {
            return self.AndThen(x => x);
        }

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
}