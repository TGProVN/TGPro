using Microsoft.EntityFrameworkCore;
using Modules.Identity.Core.Abstractions;
using Modules.Identity.Core.Entities;
using Modules.Identity.Infrastructure.Configurations;
using Shared.Infrastructure.Contexts;

namespace Modules.Identity.Infrastructure.Contexts;

public class AppIdentityDbContext : ModuleDbContext<Role, RoleClaim, User, UserToken>, IAppIdentityDbContext
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new RoleClaimConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserTokenConfiguration());
    }
}
