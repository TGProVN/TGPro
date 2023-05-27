using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Modules.Identity.Core.Abstractions.Services;
using Modules.Identity.Core.Entities;
using Modules.Identity.Core.Enums;
using Modules.Identity.Core.Helpers;
using Modules.Identity.Core.Requests;
using Modules.Identity.Core.Responses;
using Shared.Core.Abstractions.Services;
using Shared.Core.Configurations;
using Shared.Core.Constants;
using Shared.Core.Exceptions;

namespace Modules.Identity.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppConfiguration _appConfig;
    private readonly GoogleAuthConfiguration _googleAuthConfig;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly ISystemUserService _systemUser;

    public AuthenticationService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        ISystemUserService systemUser,
        IOptions<AppConfiguration> appConfig,
        IOptions<GoogleAuthConfiguration> googleAuthConfig
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _systemUser = systemUser;
        _appConfig = appConfig.Value;
        _googleAuthConfig = googleAuthConfig.Value;
    }

    public async Task<TokenResponse?> SignIn(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new UnauthorizedException(AppConstants.Messages.InvalidCredentialInfo);
        }

        if (!user.EmailConfirmed)
        {
            throw new UnauthorizedException(AppConstants.Messages.EmailUnconfirmed);
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedException(AppConstants.Messages.LockedUser);
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new UnauthorizedException(AppConstants.Messages.InvalidCredentialInfo);
        }

        return await CreateTokenResponse(user, _appConfig.DefaultLoginProvider);
    }

    public async Task<TokenResponse?> SignInWithGoogle()
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

        if (googleCredential.Token.IsExpired(SystemClock.Default))
        {
            await googleCredential.RefreshTokenAsync(CancellationToken.None);
        }

        var googleUserPayload = await GoogleJsonWebSignature.ValidateAsync(
            googleCredential.Token.IdToken,
            new GoogleJsonWebSignature.ValidationSettings {
                Audience = new[] { _googleAuthConfig.ClientId }
            });

        var user = await _userManager.FindByEmailAsync(googleUserPayload.Email);

        // @TODO - Write shared AddBasicUser method
        if (user is null)
        {
            user = new User {
                FirstName = googleUserPayload.GivenName,
                LastName = googleUserPayload.FamilyName,
                Email = googleUserPayload.Email,
                AvatarId = AppConstants.DefaultImages.MaleAvatarId,
                AvatarUrl = AppConstants.DefaultImages.MaleAvatar,
                IsActive = true,
                Gender = Gender.Undefined,
                CreatedBy = _systemUser.UserId
            };

            var addNewUser = await _userManager.CreateAsync(user, _appConfig.DefaultAppPassword);

            if (!addNewUser.Succeeded)
            {
                throw new Exception(AppConstants.Messages.InternalServerError);
            }

            var addToBasicRole = await _userManager.AddToRoleAsync(user, AppConstants.Roles.Basic);

            if (!addToBasicRole.Succeeded)
            {
                throw new Exception(AppConstants.Messages.InternalServerError);
            }
        }

        // @TODO - Implement the callback url at the presentation layer.
        return await CreateTokenResponse(user, _appConfig.GoogleLoginProvider);
    }

    private async Task<TokenResponse?> CreateTokenResponse(User user, string loginProvider)
    {
        var tokenHelpers = TokenHelpers.GetInstance(_userManager, _roleManager);
        var refreshToken = TokenHelpers.RefreshToken;

        user.UserTokens.Add(new UserToken {
            LoginProvider = loginProvider,
            Name = user.Email ?? $"{user.FirstName} {user.LastName}",
            Value = refreshToken
        });

        await _userManager.UpdateAsync(user);

        var token = await tokenHelpers.GenerateJwtToken(user, _appConfig.Secret);

        return new TokenResponse {
            Token = token,
            RefreshToken = refreshToken
        };
    }
}
