using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions;

namespace Shared.Core.Wrapper;

public class Result<T> : IResult<T>
{

    public Result(
        bool succeeded,
        string? message = default,
        T? data = default,
        IList<string>? errors = default
    ) : this(succeeded, message, data)
    {
        if (errors is not null && errors.Any())
        {
            Errors = errors;
        }
    }

    private Result(bool succeeded, string? message = default, T? data = default)
    {
        Message = message;
        Succeeded = succeeded;
        Data = data;
        Errors = new List<string>();
    }
    public string? Message { get; set; }
    public bool Succeeded { get; set; }
    public T? Data { get; set; }
    public IList<string> Errors { get; set; }
}

public class HttpResult<T> : IHttpResult<T>
{
    public IResult<T>? Value { get; set; }
    public int StatusCode { get; set; }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(Value) {
            StatusCode = StatusCode
        };

        await objectResult.ExecuteResultAsync(context);
    }

    private static IHttpResult<T> Fail(
        int statusCode,
        string? message = default,
        IList<string>? errors = default
    )
    {
        var value = new Result<T>(false, message, default, errors);

        return new HttpResult<T> {
            Value = value,
            StatusCode = statusCode
        };
    }

    public static Task<IHttpResult<T>> FailAsync(
        int statusCode,
        string? message = default,
        IList<string>? errors = default
    )
    {
        return Task.FromResult(Fail(statusCode, message, errors));
    }

    private static IHttpResult<T> Success(int statusCode, T? data, string? message = default)
    {
        var value = new Result<T>(true, message, data);

        return new HttpResult<T> {
            Value = value,
            StatusCode = statusCode
        };
    }

    public static Task<IHttpResult<T>> SuccessAsync(int statusCode, string? message = default)
    {
        return Task.FromResult(Success(statusCode, default, message));
    }

    public static Task<IHttpResult<T>> SuccessAsync(int statusCode, T data, string? message = default)
    {
        return Task.FromResult(Success(statusCode, data, message));
    }
}
