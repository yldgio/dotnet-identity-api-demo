using System.Reflection;

using ErrorOr;

using FluentValidation;

using Identity.Api.Application.Authentication.Commands;
using Identity.Api.Application.Authentication.Common;
using Identity.Api.Application.Common.Behaviors;

using MediatR;
namespace Identity.Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

}