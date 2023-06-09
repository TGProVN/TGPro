﻿using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Core.Abstractions;
using Modules.Catalog.Core.Entities;
using Modules.Catalog.Infrastructure.Configurations;
using Shared.Core.Abstractions.Services;
using Shared.Core.Constants;
using Shared.Infrastructure.Contexts;

namespace Modules.Catalog.Infrastructure.Contexts;

public class CatalogDbContext : ModuleDbContext, ICatalogDbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, ICurrentUserService currentUser)
        : base(options)
    {
        Schema = AppConstants.TableSchemas.Catalog;
        UserId = currentUser.UserId;
    }

    public override string Schema { get; }

    public DbSet<Brand>? Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new BrandConfiguration());
    }
}
