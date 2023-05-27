using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Abstractions;
using Shared.Model.Contracts;

namespace Shared.Infrastructure.Contexts;

public abstract class ModuleDbContext : AuditableDbContext, IModuleDbContext
{
    public abstract string? Schema { get; }
    protected string? UserId { get; init; }

    protected ModuleDbContext(DbContextOptions options) : base(options) {}

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (String.IsNullOrEmpty(UserId))
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }

        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTimeOffset.Now;
                    entry.Entity.CreatedBy = UserId;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = DateTimeOffset.Now;
                    entry.Entity.LastModifiedBy = UserId;
                    break;

                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                default:
                    break;
            }
        }

        return await base.SaveChangesAsync(UserId, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        if (!string.IsNullOrWhiteSpace(Schema))
        {
            builder.HasDefaultSchema(Schema);
        }

        base.OnModelCreating(builder);
        //builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}

public abstract class ModuleDbContext<TRole, TRoleClaim, TUser, TUserToken>
    : IdentityDbContext<TUser, TRole, string, IdentityUserClaim<string>,
          IdentityUserRole<string>, IdentityUserLogin<string>,
          TRoleClaim, TUserToken>, IModuleDbContext
    where TUser : IdentityUser<string>
    where TRole : IdentityRole
    where TRoleClaim : IdentityRoleClaim<string>
    where TUserToken : IdentityUserToken<string>
{
    public abstract string? Schema { get; }

    protected ModuleDbContext(DbContextOptions options) : base(options) {}

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(true, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        if (!string.IsNullOrWhiteSpace(Schema))
        {
            builder.HasDefaultSchema(Schema);
        }

        base.OnModelCreating(builder);
    }
}
