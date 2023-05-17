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
        services.IdentityModuleInitialize();
    }

    private static void IdentityModuleInitialize(this IServiceCollection services)
    {
        services.SeedModuleData<IIdentitySeeder>();
    }
}
