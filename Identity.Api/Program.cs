using Identity.Api.Domain;
using Identity.Api.Domain.Services;
using Identity.Api.Infrastructure;

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
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}