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
    // app.UseHttpsRedirection();
    app.MapControllers();
    app.MapHealthChecks("/healthz");
    app.Run();
}