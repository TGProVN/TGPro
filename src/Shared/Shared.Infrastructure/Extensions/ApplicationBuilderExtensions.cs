using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Abstractions.Services;

namespace Shared.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void SeedData<T>(this IApplicationBuilder app) where T : IDataSeeder
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var seeder = serviceScope.ServiceProvider.GetRequiredService<T>();

        seeder.Run();
    }
}
