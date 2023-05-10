using Microsoft.EntityFrameworkCore;
using Modules.Identity.Core.Abstractions;
using Modules.Identity.Core.Entities;
using Shared.Core.Constants;

namespace Modules.Identity.Infrastructure.Extensions;

public static class DatabaseContextExtensions
{
    public static async Task AddPermissionClaim(this IAppIdentityDbContext context,
                                                Role role,
                                                Tuple<string, string, IEnumerable<string?>> permissionDetail)
    {
        var claims = await context.RoleClaims.Where(x => x.RoleId == role.Id).ToListAsync();

        foreach (var permission in permissionDetail.Item3)
        {
            var exists =
                claims.Any(rc => rc.ClaimType is AppConstants.ClaimTypes.Permission && rc.ClaimValue == permission);

            if (!exists)
            {
                await context.RoleClaims.AddAsync(
                    new RoleClaim(permissionDetail.Item2, permissionDetail.Item1) {
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
