using Modules.Identity.Core.Requests;
using Modules.Identity.Core.Responses;

namespace Modules.Identity.Core.Abstractions.Services;

public interface IAuthenticationService
{
    Task<TokenResponse?> SignIn(LoginRequest request);
    Task<TokenResponse?> SignInWithGoogle();
}
