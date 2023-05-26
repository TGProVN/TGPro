using Shared.Core.Abstractions;

namespace Shared.Core.Wrapper;

public class Result : IResult
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public IList<string>? Errors { get; set; }

    private static IResult Fail(string? message = default, IList<string>? errors = default)
    {
        return new Result {
            Succeeded = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }

    public static Task<IResult> FailAsync(string? message = default, IList<string>? errors = default)
    {
        return Task.FromResult(Fail(message, errors));
    }

    private static IResult Success(string? message = default)
    {
        return new Result {
            Succeeded = true,
            Message = message
        };
    }

    public static Task<IResult> SuccessAsync(string? message = null)
    {
        return Task.FromResult(Success(message));
    }
}

public class Result<T> : IResult<T>
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public IList<string>? Errors { get; set; }
    public T? Data { get; private init; }

    private static IResult<T> Fail(string? message = default, IList<string>? errors = default)
    {
        return new Result<T> {
            Succeeded = false,
            Message = message,
            Errors = errors ?? new List<string>(),
            Data = default
        };
    }
    public static Task<IResult<T>> FailAsync(string? message = default, IList<string>? errors = default)
    {
        return Task.FromResult(Fail(message, errors));
    }

    private static IResult<T> Success(T? data, string? message = default)
    {
        return new Result<T> {
            Succeeded = true,
            Message = message,
            Errors = new List<string>(),
            Data = data
        };
    }

    public static Task<IResult<T>> SuccessAsync(string? message = default)
    {
        return Task.FromResult(Success(default, message));
    }

    public static Task<IResult<T>> SuccessAsync(T data, string? message = default)
    {
        return Task.FromResult(Success(data, message));
    }
}
