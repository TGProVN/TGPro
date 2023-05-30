using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Controllers;

namespace Shared.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddControllers()
                .ConfigureApplicationPartManager(manager => {
                     manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                 });
    }

    public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services, string connectionString)
        where T : DbContext
    {
        services.AddDbContext<T>(optionsBuilder => {
            optionsBuilder.UseSqlServer(connectionString, sqlOptionsBuilder => {
                sqlOptionsBuilder.MigrationsAssembly(typeof(T).Assembly.FullName);
            });
        });

        return services;
    }
}
