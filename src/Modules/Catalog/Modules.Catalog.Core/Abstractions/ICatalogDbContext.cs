using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Core.Entities;
using Shared.Core.Abstractions;

namespace Modules.Catalog.Core.Abstractions;

public interface ICatalogDbContext : IModuleDbContext
{
    DbSet<Brand>? Brands { get; set; }
}
