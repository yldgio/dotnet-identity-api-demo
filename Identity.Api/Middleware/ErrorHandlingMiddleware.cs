using System.Net;
using System.Text.Json;

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
        var code = HttpStatusCode.InternalServerError;
        var problemDetails = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "Error processing your request",
            Instance = context.Request.Path,
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = e.Message
        };
        var result = JsonSerializer.Serialize(problemDetails);
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}