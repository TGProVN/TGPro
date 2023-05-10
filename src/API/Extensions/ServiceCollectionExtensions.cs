using API.OpenApi;
using API.Services;
using Asp.Versioning;
using Microsoft.Extensions.Options;
using Modules.Catalog.Extensions;
using Modules.Identity.Extensions;
using Shared.Core.Abstractions.Services;
using Shared.Core.Configurations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    public static AppConfiguration GetAppConfigurations(this IServiceCollection services,
                                                        IConfiguration configuration)
    {
        var appConfig = configuration.GetSection(nameof(AppConfiguration));
        services.Configure<AppConfiguration>(appConfig);
        return appConfig.Get<AppConfiguration>() ?? throw new InvalidOperationException();
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
    }

    public static void RegisterApiVersioning(this IServiceCollection services, AppConfiguration appConfig)
    {
        services
           .AddApiVersioning(options => {
                options.DefaultApiVersion = new ApiVersion(appConfig.ApiMajorVersion, appConfig.ApiMinorVersion);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
           .AddApiExplorer(options => {
                options.GroupNameFormat = appConfig.ApiVersionGroupNameFormat;
                options.SubstituteApiVersionInUrl = true;
            });
    }

    public static void AddCurrentUserService(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }

    public static void RegisterAppModules(this IServiceCollection services, IConfiguration config)
    {
        services.AddCatalogModule(config);
        services.AddIdentityModule(config);
    }
}
