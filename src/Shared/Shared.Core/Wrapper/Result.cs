using Shared.Core.Abstractions;

namespace Shared.Core.Wrapper;

public class Result : IResult
{
    public string? Message { get; set; }
    public bool Succeeded { get; set; }
    public IList<string?> Errors { get; set; } = new List<string?>();

    public static IResult Fail(string? message = null, IList<string?>? errors = null)
    {
        var result = new Result {
            Succeeded = false
        };

        if (!String.IsNullOrEmpty(message))
        {
            result.Message = message;
        }

        if (errors is not null && errors.Any())
        {
            result.Errors = errors;
        }

        return result;
    }

    public static Task<IResult> FailAsync(string? message = null, IList<string?>? errors = null)
    {
        return Task.FromResult(Fail(message, errors));
    }

    public static IResult Success(string? message = null)
    {
        if (String.IsNullOrEmpty(message))
        {
            return new Result { Succeeded = true };
        }

        return new Result {
            Message = message,
            Succeeded = true
        };
    }

    public static Task<IResult> SuccessAsync(string? message = null)
    {
        return Task.FromResult(Success(message));
    }
}

public class Result<T> : Result, IResult<T>
{
    public T? Data { get; private init; }

    public new static Result<T> Fail(string? message = null, IList<string?>? errors = null)
    {
        var result = new Result<T> {
            Succeeded = false
        };

        if (!String.IsNullOrEmpty(message))
        {
            result.Message = message;
        }

        if (errors is not null && errors.Any())
        {
            result.Errors = errors;
        }

        return result;
    }

    public new static Task<Result<T>> FailAsync(string? message = null, IList<string?>? errors = null)
    {
        return Task.FromResult(Fail(message, errors));
    }

    public new static Result<T> Success(string? message = null)
    {
        if (String.IsNullOrEmpty(message))
        {
            return new Result<T> { Succeeded = true };
        }

        return new Result<T> {
            Message = message,
            Succeeded = true
        };
    }

    public static Result<T> Success(T data, string? message = null)
    {
        if (String.IsNullOrEmpty(message))
        {
            return new Result<T> {
                Succeeded = true,
                Data = data
            };
        }

        return new Result<T> {
            Message = message,
            Succeeded = true,
            Data = data
        };
    }

    public new static Task<Result<T>> SuccessAsync(string? message = null)
    {
        return Task.FromResult(Success(message));
    }

    public static Task<Result<T>> SuccessAsync(T data, string? message = null)
    {
        return Task.FromResult(Success(data, message));
    }
}
