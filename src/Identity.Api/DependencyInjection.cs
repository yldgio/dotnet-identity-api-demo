using Identity.Api.Presentation;

namespace Identity.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationDependencies(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddMappings();
        return services;
    }

}