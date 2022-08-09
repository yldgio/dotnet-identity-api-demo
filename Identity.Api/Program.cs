using System;
using System.Runtime.Intrinsics.X86;

using Identity.Api.Domain;
using Identity.Api.Filters;
using Identity.Api.Infrastructure;

using Microsoft.AspNetCore.Diagnostics;
//using Identity.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    builder.Services
        .AddDomainDependencies()
        .AddInfrastructureDependencies(builder.Configuration);

    // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler(errorHandlingPath: "/error");

    // //alternatively we can map the handler locally:
    // app.Map("/error", (HttpContext context) =>
    // {
    //     //accessing the exception
    //     Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    // //no dependency injection
    //     //the Results Problem factory allows you to pass Extensions to customize the return values
    //     return Results.Problem();
    // });
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}