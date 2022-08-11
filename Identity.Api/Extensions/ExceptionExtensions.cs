using System.Net;

using Identity.Api.Application.Common.Exceptions;

using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Identity.Api.Extensions;
public static class ExceptionExtensions
{
    public static ProblemDetails ProblemDetails(this HttpContext context, Exception exception)
    {
        var (code, message) = exception.MapToCodeAndMessage();
        var problemDetailsFactory = context.RequestServices?.GetRequiredService<ProblemDetailsFactory>();
        return problemDetailsFactory?.CreateProblemDetails(context, statusCode: (int)code, title: message)!;
    }
    public static (HttpStatusCode, string) MapToCodeAndMessage(this Exception exception)
    {
        return exception switch
        {
            IApplicationException domainException => (domainException.StatusCode, domainException.ErrorMessage),
            _ => (HttpStatusCode.InternalServerError, "an error occurred.")
        };
        //return (ProblemDetails)Results.Problem(statusCode: (int)statusCode, title: message);
    }
}