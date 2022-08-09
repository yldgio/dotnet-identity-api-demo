using System.Net;
using System.Text.Json;

using Identity.Api.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {

            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception e)
    {

        var problemDetails = context.ProblemDetails(e);
        var result = JsonSerializer.Serialize(problemDetails);
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)problemDetails.Status!;
        return context.Response.WriteAsync(result);
    }
}