namespace RustSharp
{
    /// <summary>
    /// <see cref="Result{TValue, TError}"/> is a type that represents either success (<see cref="OkResult{TValue, TError}"/>) or failure (<see cref="ErrResult{TValue, TError}"/>).
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public abstract class Result<TValue, TError> : ICloneable
    {
        public bool IsOk => this is OkResult<TValue, TError>;
        public bool IsErr => this is ErrResult<TValue, TError>;

        public bool IsOkAnd(Func<bool> f)
        {
            if (!IsOk)
                return false;
            return f.Invoke();
        }

        public bool IsErrAnd(Func<bool> f)
        {
            if (!IsErr)
                return false;
            return f.Invoke();
        }

        public Option<TValue> Ok()
        {
            if (this is OkResult<TValue, TError> ok)
                return Option<TValue>.Some(ok.Value);
            return Option<TValue>.None();
        }

        public Option<TError> Err()
        {
            if (this is ErrResult<TValue, TError> err)
                return Option<TError>.Some(err.Value);
            return Option<TError>.None();
        }

        public Result<TNewValue, TError> Map<TNewValue>(Func<TValue, TNewValue> mapper)
        {
            if (this is OkResult<TValue, TError> ok)
                return Result<TNewValue, TError>.Ok(mapper.Invoke(ok.Value));
            else if (this is ErrResult<TValue, TError> err)
                return Result<TNewValue, TError>.Err(err.Value);
            throw new InvalidOperationException();
        }

        public TNewValue MapOr<TNewValue>(TNewValue defaultValue, Func<TValue, TNewValue> mapper)
        {
            if (this is OkResult<TValue, TError> ok)
                return mapper.Invoke(ok.Value);
            return defaultValue;
        }

        public TNewValue MapOrElse<TNewValue>(Func<TError, TNewValue> errorMapper, Func<TValue, TNewValue> mapper)
        {
            if (this is OkResult<TValue, TError> ok)
                return mapper.Invoke(ok.Value);
            else if (this is ErrResult<TValue, TError> err)
                return errorMapper.Invoke(err.Value);
            throw new InvalidOperationException();
        }

        public Result<TValue, TNewError> MapErr<TNewError>(Func<TError, TNewError> mapper)
        {
            if (this is OkResult<TValue, TError> ok)
                return Result<TValue, TNewError>.Ok(ok.Value);
            else if (this is ErrResult<TValue, TError> err)
                return Result<TValue, TNewError>.Err(mapper.Invoke(err.Value));
            throw new InvalidOperationException();
        }

        public Result<TValue, TError> Inspect(Action<TValue> action)
        {
            if (this is OkResult<TValue, TError> ok)
                action.Invoke(ok.Value);

            return this;
        }

        public Result<TValue, TError> InspectErr(Action<TError> action)
        {
            if (this is ErrResult<TValue, TError> err)
                action.Invoke(err.Value);

            return this;
        }

        public TValue Expect(string msg)
        {
            if (this is OkResult<TValue, TError> ok)
                return ok.Value;
            else if (this is ErrResult<TValue, TError> err)
                throw UnwrapException.New(msg, err.Value);
            throw new InvalidOperationException();
        }

        public TValue Unwrap()
        {
            if (this is OkResult<TValue, TError> ok)
                return ok.Value;
            else if (this is ErrResult<TValue, TError> err)
                throw UnwrapException.New("Called Result.Unwrap on an Err value", err.Value);
            throw new InvalidOperationException();
        }

        public TValue? UnwrapOrDefault()
        {
            if (this is OkResult<TValue, TError> ok)
                return ok.Value;
            else if (this is ErrResult<TValue, TError> err)
                return default;
            throw new InvalidOperationException();
        }

        public TError ExpectErr(string msg)
        {
            if (this is OkResult<TValue, TError> ok)
                throw UnwrapException.New(msg, ok.Value);
            if (this is ErrResult<TValue, TError> err)
                return err.Value;
            throw new InvalidOperationException();
        }

        public TError UnwrapErr()
        {
            if (this is OkResult<TValue, TError> ok)
                throw UnwrapException.New($"Called Result.UnwrapErr on an Ok value", ok.Value);
            if (this is ErrResult<TValue, TError> err)
                return err.Value;
            throw new InvalidOperationException();
        }

        public Result<TNewValue, TError> And<TNewValue>(Result<TNewValue, TError> res)
        {
            if (this is OkResult<TValue, TError>)
                return res;
            else if (this is ErrResult<TValue, TError> err)
                return Result<TNewValue, TError>.Err(err.Value);
            throw new InvalidOperationException();
        }

        public Result<TNewValue, TError> AndThen<TNewValue>(Func<TValue, Result<TNewValue, TError>> operation)
        {
            if (this is OkResult<TValue, TError> ok)
                return operation.Invoke(ok.Value);
            else if (this is ErrResult<TValue, TError> err)
                return Result<TNewValue, TError>.Err(err.Value);
            throw new InvalidOperationException();
        }

        public Result<TValue, TError> Or(Result<TValue, TError> res)
        {
            if (this is OkResult<TValue, TError> ok)
                return ok;
            else if (this is ErrResult<TValue, TError> err)
                return res;
            throw new InvalidOperationException();
        }

        public Result<TValue, TNewError> OrElse<TNewError>(Func<TError, Result<TValue, TNewError>> operation)
        {
            if (this is OkResult<TValue, TError> ok)
                return Result<TValue, TNewError>.Ok(ok.Value);
            else if (this is ErrResult<TValue, TError> err)
                return operation.Invoke(err.Value);
            throw new InvalidOperationException();
        }

        public TValue UnwrapOr(TValue defaultValue)
        {
            if (this is OkResult<TValue, TError> ok)
                return ok.Value;
            else if (this is ErrResult<TValue, TError> err)
                return defaultValue;
            throw new InvalidOperationException();
        }

        public TValue UnwrapOrElse(Func<TError, TValue> operation)
        {
            if (this is OkResult<TValue, TError> ok)
                return ok.Value;
            else if (this is ErrResult<TValue, TError> err)
                return operation.Invoke(err.Value);
            throw new InvalidOperationException();
        }

        public bool Contains<TOther>(TOther x) where TOther : IEquatable<TValue>
        {
            if (this is OkResult<TValue, TError> ok)
                return x.Equals(ok.Value);

            return false;
        }

        public bool ContainsErr<TOther>(TOther f) where TOther : IEquatable<TError>
        {
            if (this is ErrResult<TValue, TError> err)
                return f.Equals(err.Value);

            return false;
        }

        public void Match(Action<TValue> okAction, Action<TError> errAction)
        {
            if (this is OkResult<TValue, TError> ok)
                okAction.Invoke(ok.Value);
            else if (this is ErrResult<TValue, TError> err)
                errAction.Invoke(err.Value);
        }

        public Result<TValue, TError> Clone()
        {
            if (this is OkResult<TValue, TError> ok)
            {
                TValue newValue = ok.Value;
                if (newValue is ICloneable cloneableNewValue)
                    newValue = (TValue)cloneableNewValue.Clone();
                return Ok(newValue);
            }

            return this;
        }

        public static OkResult<TValue, TError> Ok(TValue value) => new OkResult<TValue, TError>(value);
        public static ErrResult<TValue, TError> Err(TError error) => new ErrResult<TValue, TError>(error);

        object ICloneable.Clone() => Clone();
    }
}