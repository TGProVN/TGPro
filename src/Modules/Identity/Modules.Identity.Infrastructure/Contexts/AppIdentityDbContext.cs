using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modules.Identity.Core.Abstractions;
using Modules.Identity.Core.Entities;
using Modules.Identity.Infrastructure.Configurations;
using Shared.Core.Constants;
using Shared.Infrastructure.Contexts;

namespace Modules.Identity.Infrastructure.Contexts;

public class AppIdentityDbContext : ModuleDbContext<Role, RoleClaim, User, UserToken>, IAppIdentityDbContext
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
    {
        Schema = AppConstants.TableSchemas.Identity;
    }

    public override string? Schema { get; }

    public override DbSet<Role> Roles {
        get => Set<Role>();
    }

    public override DbSet<RoleClaim> RoleClaims {
        get => Set<RoleClaim>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityUserRole<string>>(entity => {
            entity.ToTable("UserRoles", Schema);
        });
        builder.Entity<IdentityUserClaim<string>>(entity => {
            entity.ToTable("UserClaims", Schema);
        });
        builder.Entity<IdentityUserLogin<string>>(entity => {
            entity.ToTable("UserLogins", Schema);
        });

        // Custom Identity default table
        builder.ApplyConfiguration(new RoleClaimConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserTokenConfiguration());
    }
}
