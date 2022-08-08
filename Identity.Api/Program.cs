using Identity.Api.Domain;
using Identity.Api.Filters;
using Identity.Api.Infrastructure;
//using Identity.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services
        .AddDomainDependencies()
        .AddInfrastructureDependencies(builder.Configuration);

    builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}