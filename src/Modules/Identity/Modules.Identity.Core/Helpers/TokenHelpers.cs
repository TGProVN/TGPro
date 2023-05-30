using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Modules.Identity.Core.Entities;
using Modules.Identity.Core.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Modules.Identity.Core.Helpers;

public sealed class TokenHelpers
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private static TokenHelpers? _instance;

    public static string RefreshToken {
        get => GenerateRefreshToken();
    }

    private TokenHelpers(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public static TokenHelpers GetInstance(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        return _instance ??= new TokenHelpers(userManager, roleManager);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    public async Task<string> GenerateJwtToken(User user, string secret)
    {
        var credentials = GetSigningCredentials(secret);
        var claims = await _userManager.GetUserClaims(user, _roleManager);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(2),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private static SigningCredentials GetSigningCredentials(string secret)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        return new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
    }
}
