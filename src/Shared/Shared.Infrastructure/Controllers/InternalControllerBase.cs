using Microsoft.AspNetCore.Mvc;
using Shared.Core.Abstractions;
using Shared.Core.Constants;
using Shared.Core.Wrapper;

namespace Shared.Infrastructure.Controllers;

public abstract class InternalControllerBase : ControllerBase, IControllerBase
{
    public virtual IActionResult HandleResult<T>(int statusCode, T result)
    {
        if (result is not null && !result.Equals(default))
        {
            return statusCode switch {
                AppConstants.StatusCode.Created => Created(String.Empty, Result<T>.SuccessAsync(result, "Created")),
                AppConstants.StatusCode.Accepted => Accepted(Result<T>.SuccessAsync(result, "Accepted")),
                AppConstants.StatusCode.NoContent => NoContent(),
                AppConstants.StatusCode.PartialContent => throw new NotImplementedException(),
                _ => Ok(result)
            };
        }

        if (result is null && typeof(T) != typeof(bool))
        {
            return NotFound(Result<T>.FailAsync("Not Found!"));
        }

        return BadRequest(Result<T>.FailAsync("Bad Request!"));
    }
}
