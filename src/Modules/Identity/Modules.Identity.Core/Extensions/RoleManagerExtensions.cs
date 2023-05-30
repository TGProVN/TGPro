using Microsoft.AspNetCore.Identity;
using Modules.Identity.Core.Entities;
using System.Security.Claims;

namespace Modules.Identity.Core.Extensions;

public static class RoleManagerExtensions
{
    public static async Task<IEnumerable<Claim>?> GetPermissionClaims(this RoleManager<Role> roleManager, string role)
    {
        var roleEntity = await roleManager.FindByNameAsync(role);

        if (roleEntity is null)
        {
            return null;
        }

        return await roleManager.GetClaimsAsync(roleEntity);
    }
}
