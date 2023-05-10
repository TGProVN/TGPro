using Microsoft.EntityFrameworkCore;
using Modules.Identity.Core.Entities;
using Shared.Core.Abstractions;

namespace Modules.Identity.Core.Abstractions;

public interface IAppIdentityDbContext : IModuleDbContext
{
    DbSet<Role> Roles { get; set; }
    DbSet<RoleClaim> RoleClaims { get; set; }
}
