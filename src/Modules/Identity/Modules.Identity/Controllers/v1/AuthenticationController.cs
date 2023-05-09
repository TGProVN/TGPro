using Microsoft.AspNetCore.Mvc;
using Modules.Identity.Core.Abstractions.Services;
using Modules.Identity.Core.Requests;

namespace Modules.Identity.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
    {
        return await _authService.SignIn(request);
    }

    [HttpGet("test-route")]
    public async Task<IActionResult> Test()
    {
        return await _authService.Test();
    }
}
