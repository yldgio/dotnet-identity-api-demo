using Identity.Api.Domain.Services;

namespace Identity.Api.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }

}