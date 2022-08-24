using Identity.Api;
using Identity.Api.Application;
using Identity.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services
        .AddPresentationDependencies()
        .AddApplicationDependencies()
        .AddInfrastructureDependencies(builder.Configuration)
        .AddHealthChecks();
}

var app = builder.Build();
{
    app.UseExceptionHandler(errorHandlingPath: "/error");
    if (app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
    }
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.MapHealthChecks("/healthz");
    app.Run();
}

public partial class Program { }