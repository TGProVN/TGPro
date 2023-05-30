using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Core.Abstractions;
using Modules.Catalog.Infrastructure.Contexts;
using Shared.Infrastructure.Extensions;

namespace Modules.Catalog.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCatalogInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        if (String.IsNullOrWhiteSpace(connectionString))
        {
            throw new NullReferenceException("DB Connection String is null!");
        }

        services
           .AddDatabaseContext<CatalogDbContext>(connectionString)
           .AddScoped<ICatalogDbContext>(
                provider => provider.GetService<CatalogDbContext>() ??
                            throw new NullReferenceException("Could not get CatalogDbContext service!"));
    }
}
