using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Abstractions.Services;
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

        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();

        dbContext.Database.Migrate();

        return services;
    }

    public static void SeedModuleData<T>(this IServiceCollection services) where T : IDataSeeder
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<T>();

        seeder.Run();
    }
}
