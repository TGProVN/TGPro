using Microsoft.AspNetCore.Identity;
using Modules.Identity.Core.Entities;
using System.Security.Claims;

namespace Modules.Identity.Core.Extensions;

public static class UserManagerExtensions
{
    public static async Task<IEnumerable<Claim>> GetUserClaims(this UserManager<User> userManager,
                                                               User user,
                                                               RoleManager<Role> roleManager)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();
        var permissionClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
            var permissions = await roleManager.GetPermissionClaims(role);

            if (permissions is null)
            {
                continue;
            }

            permissionClaims.AddRange(permissions);
        }

        var result = new List<Claim> {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}")
        };

        return result.Union(userClaims).Union(roleClaims).Union(permissionClaims);
    }
}
