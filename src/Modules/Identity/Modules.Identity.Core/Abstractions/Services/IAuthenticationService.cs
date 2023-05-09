using Modules.Identity.Core.Requests;
using Modules.Identity.Core.Responses;
using Shared.Core.Abstractions;

namespace Modules.Identity.Core.Abstractions.Services;

public interface IAuthenticationService
{
    Task<IHttpResult<TokenResponse?>> SignIn(LoginRequest request);
    Task<IHttpResult<TokenResponse?>> SignInWithGoogle();
    Task<IHttpResult<string>> Test();
}
