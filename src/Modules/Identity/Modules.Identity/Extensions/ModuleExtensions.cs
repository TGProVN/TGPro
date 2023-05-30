using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.Core.Abstractions.Services;
using Modules.Identity.Infrastructure.Extensions;
using Shared.Infrastructure.Extensions;

namespace Modules.Identity.Extensions;

public static class ModuleExtensions
{
    public static void AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityInfrastructure(configuration);
    }

    public static void IdentityModuleInitialize(this IApplicationBuilder app)
    {
        app.SeedData<IIdentitySeeder>();
    }
}
