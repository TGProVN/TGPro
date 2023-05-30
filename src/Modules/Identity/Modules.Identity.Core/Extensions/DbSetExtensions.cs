using Microsoft.EntityFrameworkCore;
using Modules.Identity.Core.Entities;
using Shared.Core.Constants;
using Shared.Core.Structs;

namespace Modules.Identity.Core.Extensions;

public static class DbSetExtensions
{
    public static async Task AddPermissionClaim(this DbSet<RoleClaim> roleClaims,
                                                Role role,
                                                PermissionDetail permissionDetail)
    {
        const string claimType = AppConstants.ClaimTypes.Permission;
        var claims = await roleClaims.Where(x => x.RoleId == role.Id).ToListAsync();

        foreach (var permission in permissionDetail.Permissions)
        {
            var exists = claims.Any(x => x.ClaimType is claimType && x.ClaimValue == permission);

            if (!exists)
            {
                await roleClaims.AddAsync(
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
