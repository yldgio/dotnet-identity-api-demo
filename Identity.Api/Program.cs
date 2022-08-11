using Identity.Api;
using Identity.Api.Application;
using Identity.Api.Filters;
using Identity.Api.Infrastructure;
using Identity.Api.Middleware;
//using Identity.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services
        .AddPresentationDependencies()
        .AddApplicationDependencies()
        .AddInfrastructureDependencies(builder.Configuration);

}

var app = builder.Build();
{
    app.UseExceptionHandler(errorHandlingPath: "/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}