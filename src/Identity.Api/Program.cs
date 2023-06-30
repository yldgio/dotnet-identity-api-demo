using Identity.Api;
using Identity.Api.Application;
using Identity.Api.Infrastructure;
using RendleLabs.OpenApi.Web;
using System.Reflection;

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
    if (app.Environment.IsDevelopment())
    {
        app.UseStaticOpenApi(Assembly.GetExecutingAssembly(), "Identity.Api.openapi.yml", new StaticOpenApiOptions
        {
            Version = 1,
            UiPathPrefix = "swagger",
            JsonPath = "swagger/v1/openapi.json",
            YamlPath = "swagger/v1/openapi.yaml",
            Redoc = {
                Path = "swagger/v1/redocs"
            },
            Elements =
            {
                Path = "swagger/v1/docs"
            }
        });
    }
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