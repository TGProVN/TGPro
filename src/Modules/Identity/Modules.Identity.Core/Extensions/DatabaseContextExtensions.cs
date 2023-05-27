using Microsoft.EntityFrameworkCore;
using Modules.Identity.Core.Abstractions;
using Modules.Identity.Core.Entities;
using Shared.Core.Constants;
using Shared.Core.Structs;

namespace Modules.Identity.Core.Extensions;

public static class DatabaseContextExtensions
{
    public static async Task AddPermissionClaim(this IAppIdentityDbContext context,
                                                Role role,
                                                PermissionDetail permissionDetail)
    {
        const string claimType = AppConstants.ClaimTypes.Permission;
        var claims = await context.RoleClaims.Where(x => x.RoleId == role.Id).ToListAsync();

        foreach (var permission in permissionDetail.Permissions)
        {
            var exists = claims.Any(x => x.ClaimType is claimType && x.ClaimValue == permission);

            if (!exists)
            {
                await context.RoleClaims.AddAsync(
                    new RoleClaim(permissionDetail.Description, permissionDetail.Group) {
                        ClaimType = AppConstants.ClaimTypes.Permission,
                        ClaimValue = permission,
                        RoleId = role.Id,
                        CreatedBy = role.CreatedBy
                    }
                );
            }
        }
    }
}
