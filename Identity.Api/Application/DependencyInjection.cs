using Identity.Api.Application.Services;
using Identity.Api.Application.Services.Commands;
using Identity.Api.Application.Services.Queries;

using MediatR;
namespace Identity.Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // services.AddScoped<IAuthCommandService, AuthCommandService>();
        // services.AddScoped<IAuthQueryService, AuthQueryService>();
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        return services;
    }

}