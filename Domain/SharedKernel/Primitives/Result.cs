namespace Domain.SharedKernel.Primitives;

public record struct Result<TValue>
{
    private readonly TValue? _value = default;
    private readonly List<Error>? _errors = null;

    public bool IsError { get; }

    public List<Error> Errors => IsError ? _errors! : new List<Error> { NoErrors };

    public List<Error> ErrorsOrEmptyList => IsError ? _errors! : new();

    public TValue Value => _value!;

    public Error FirstError
    {
        get
        {
            if (!IsError)
            {
                return NoFirstError;
            }

            return _errors![0];
        }
    }

    public void Switch(Action<TValue> onValue, Action<List<Error>> onError)
    {
        if (IsError)
        {
            onError(Errors);
            return;
        }

        onValue(Value);
    }

    public void SwitchFirst(Action<TValue> onValue, Action<Error> onFirstError)
    {
        if (IsError)
        {
            onFirstError(FirstError);
            return;
        }

        onValue(Value);
    }

    public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<List<Error>, TResult> onError)
    {
        if (IsError)
        {
            return onError(Errors);
        }

        return onValue(Value);
    }

    private Result(Error error)
    {
        _errors = new List<Error> { error };
        IsError = true;
    }

    private Result(List<Error> errors)
    {
        _errors = errors;
        IsError = true;
    }

    private Result(TValue value)
    {
        _value = value;
        IsError = false;
    }


    private static readonly Error NoFirstError = Error.Unexpected(
        code: "Result.NoFirstError",
        description: "First error cannot be retrieved from a successful Result.");

    private static readonly Error NoErrors = Error.Unexpected(
        code: "Result.NoErrors",
        description: "Error list cannot be retrieved from a successful Result.");

    public static Result<TValue> From(List<Error> errors)
    {
        return errors;
    }

    public static implicit operator Result<TValue>(TValue value)
    {
        return new Result<TValue>(value);
    }

    public static implicit operator Result<TValue>(Error error)
    {
        return new Result<TValue>(error);
    }

    public static implicit operator Result<TValue>(List<Error> errors)
    {
        return new Result<TValue>(errors);
    }

    public static implicit operator Result<TValue>(Error[] errors)
    {
        return new Result<TValue>(errors.ToList());
    }
}

public class Result
{
    public static Result<TValue> From<TValue>(TValue value)
    {
        return value;
    }
}
