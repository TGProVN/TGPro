using Microsoft.AspNetCore.Mvc;

namespace Shared.Core.Abstractions;

public interface IResult<T>
{
    bool Succeeded { get; set; }
    string? Message { get; set; }
    T? Data { get; set; }
    IList<string> Errors { get; set; }
}

public interface IHttpResult<T> : IActionResult
{
    IResult<T>? Value { get; set; }
    int StatusCode { get; set; }
}
