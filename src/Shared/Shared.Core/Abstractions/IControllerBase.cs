using Microsoft.AspNetCore.Mvc;

namespace Shared.Core.Abstractions;

public interface IControllerBase
{
    IActionResult HandleResult<T>(int statusCode, T result);
}
