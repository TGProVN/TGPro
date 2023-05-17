using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.Core.Abstractions;
using Modules.Identity.Core.Abstractions.Services;
using Modules.Identity.Core.Entities;
using Modules.Identity.Infrastructure.Contexts;
using Modules.Identity.Infrastructure.Services;
using Shared.Core.Abstractions.Services;
using Shared.Infrastructure.Extensions;

namespace Modules.Identity.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        if (String.IsNullOrWhiteSpace(connectionString))
        {
            throw new NullReferenceException("DB Connection String is null!");
        }

        services
           .AddDatabaseContext<AppIdentityDbContext>(connectionString)
           .AddIdentity<User, Role>(options => {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
           .AddEntityFrameworkStores<AppIdentityDbContext>()
           .AddDefaultTokenProviders();

        services.AddScoped<IAppIdentityDbContext>(
            provider => provider.GetService<AppIdentityDbContext>() ??
                        throw new NullReferenceException("Could not get AppIdentityDbContext service!")
        );

        services.AddIdentityServices();
    }

    private static void AddIdentityServices(this IServiceCollection services)
    {
        services
           .AddTransient<IAuthenticationService, AuthenticationService>()
           .AddTransient<IIdentitySeeder, IdentitySeeder>()
           .AddTransient<ISystemUserService, SystemUserService>();
    }
}
