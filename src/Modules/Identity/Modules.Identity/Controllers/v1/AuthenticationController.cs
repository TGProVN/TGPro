using Microsoft.AspNetCore.Mvc;
using Modules.Identity.Core.Abstractions.Services;
using Modules.Identity.Core.Requests;
using Shared.Core.Constants;

namespace Modules.Identity.Controllers.v1;

public class AuthenticationController : IdentityControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
    {
        return HandleResult(AppConstants.StatusCode.Ok, await _authService.SignIn(request));
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleSignIn()
    {
        return HandleResult(AppConstants.StatusCode.Ok, await _authService.SignInWithGoogle());
    }
}
