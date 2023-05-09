using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modules.Identity.Core.Abstractions.Services;
using Modules.Identity.Core.Entities;
using Modules.Identity.Core.Requests;
using Modules.Identity.Core.Responses;
using Shared.Core.Abstractions;
using Shared.Core.Configurations;
using Shared.Core.Constants;
using Shared.Core.Wrapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Modules.Identity.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppConfiguration _appConfig;
    private readonly GoogleAuthConfiguration _googleAuthConfig;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;

    public AuthenticationService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IOptions<AppConfiguration> appConfig,
        IOptions<GoogleAuthConfiguration> googleAuthConfig
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _appConfig = appConfig.Value;
        _googleAuthConfig = googleAuthConfig.Value;
    }

    public async Task<IHttpResult<string>> Test()
    {
        return await HttpResult<string>.SuccessAsync(
            AppConstants.StatusCode.Ok,
            "Test Route!",
            "Default Message"
        );
    }

    public async Task<IHttpResult<TokenResponse?>> SignIn(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return await HttpResult<TokenResponse?>.FailAsync(
                AppConstants.StatusCode.Unauthorized,
                AppConstants.Messages.InvalidCredentialInfo
            );
        }

        if (!user.EmailConfirmed)
        {
            return await HttpResult<TokenResponse?>.FailAsync(
                AppConstants.StatusCode.Unauthorized,
                AppConstants.Messages.EmailUnconfirmed
            );
        }

        if (!user.IsActive)
        {
            return await HttpResult<TokenResponse?>.FailAsync(
                AppConstants.StatusCode.Unauthorized,
                AppConstants.Messages.LockedUser
            );
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return await HttpResult<TokenResponse?>.FailAsync(
                AppConstants.StatusCode.Unauthorized,
                AppConstants.Messages.InvalidCredentialInfo
            );
        }

        return await CreateTokenResponse(user, _appConfig.DefaultLoginProvider);
    }

    public async Task<IHttpResult<TokenResponse?>> SignInWithGoogle()
    {
        var googleCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets {
                ClientId = _googleAuthConfig.ClientId,
                ClientSecret = _googleAuthConfig.ClientSecret
            },
            AppConstants.GoogleScopes,
            "Google User",
            CancellationToken.None
        );

        await RefreshGoogleToken(googleCredential);

        var googleUserPayload = await GoogleJsonWebSignature.ValidateAsync(
            googleCredential.Token.IdToken,
            new GoogleJsonWebSignature.ValidationSettings {
                Audience = new[] { _googleAuthConfig.ClientId }
            });

        var user = await _userManager.FindByEmailAsync(googleUserPayload.Email);

        if (user is null)
        {
            // @TODO - add user to DB
            throw new NotImplementedException();
        }

        // @TODO - Implement the callback url at the presentation layer.
        return await CreateTokenResponse(user, _appConfig.GoogleLoginProvider);
    }

    private async Task<IHttpResult<TokenResponse?>> CreateTokenResponse(User user, string loginProvider)
    {
        var refreshToken = GenerateRefreshToken();

        user.UserTokens.Add(new UserToken {
            LoginProvider = loginProvider,
            Name = user.Email ?? $"{user.FirstName} {user.LastName}",
            Value = refreshToken
        });

        await _userManager.UpdateAsync(user);

        var token = await GenerateJwtToken(user);

        return await HttpResult<TokenResponse?>.SuccessAsync(
            AppConstants.StatusCode.Created,
            new TokenResponse {
                Token = token,
                RefreshToken = refreshToken
            }
        );
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    private async Task<string> GenerateJwtToken(User user)
    {
        var credentials = GetSigningCredentials();
        var claims = await GetClaims(user);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var secret = Encoding.UTF8.GetBytes(_appConfig.Secret);
        var key = new SymmetricSecurityKey(secret);

        return new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
    }

    private async Task<IEnumerable<Claim>> GetClaims(User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();
        var permissionClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
            var roleEntity = await _roleManager.FindByNameAsync(role);

            if (roleEntity is null)
            {
                continue;
            }

            var permissions = await _roleManager.GetClaimsAsync(roleEntity);
            permissionClaims.AddRange(permissions);
        }

        var result = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}")
        };

        return result.Union(userClaims).Union(roleClaims).Union(permissionClaims);
    }

    private static async Task<bool> RefreshGoogleToken(UserCredential userCredential)
    {
        if (userCredential.Token.IsExpired(SystemClock.Default))
        {
            return await userCredential.RefreshTokenAsync(CancellationToken.None);
        }

        return true;
    }
}
