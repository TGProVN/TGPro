using API.Extensions;
using Shared.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
var services = builder.Services;

services.AddSharedInfrastructure();
services.AddSwagger();
services.RegisterApiVersioning();
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
app.UseAuthorization();
app.MapControllers();

app.Run();
