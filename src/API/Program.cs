using API.Extensions;
using API.Middlewares;
using Shared.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
var appConfig = services.GetAppConfigurations(configuration);

services.AddSharedInfrastructure();
services.AddSwagger();
services.RegisterApiVersioning(appConfig);
services.AddCurrentUserService();
services.RegisterAppModules(configuration);
services.AddEndpointsApiExplorer();
services.Configure<RouteOptions>(options => {
    options.LowercaseUrls = true;
});

var app = builder.Build();
var env = app.Environment;

app.UseHttpsRedirection();
app.UseExceptionHandling(env);
app.ConfigureSwagger();
app.UseMiddleware<ErrorHandler>();
app.UseAuthorization();
app.MapControllers();
app.Initialize();

app.Run();
