using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Modules.Identity.Core.Entities;
using Shared.Core.Abstractions;

namespace Modules.Identity.Core.Abstractions;

public interface IAppIdentityDbContext : IModuleDbContext
{
    DatabaseFacade Database { get; }
    DbSet<Role> Roles { get; }
    DbSet<RoleClaim> RoleClaims { get; }
}
