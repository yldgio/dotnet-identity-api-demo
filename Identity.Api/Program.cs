using Identity.Api.Domain;
using Identity.Api.Domain.Services;
using Identity.Api.Infrastructure;
using Identity.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services
        .AddDomainDependencies()
        .AddInfrastructureDependencies(builder.Configuration);

    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}