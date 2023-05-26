using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.Controllers;

namespace Modules.Identity.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class IdentityControllerBase : InternalControllerBase
{
    // TODO - Override base HandleResult method to handle identity result if needed
    // public override IActionResult HandleResult<T>(int statusCode, T result)
    // {
    //     return base.HandleResult(statusCode, result);
    // }
}
