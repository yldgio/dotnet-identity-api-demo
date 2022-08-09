using System.Net;

using Identity.Api.Domain.Common.Exceptions;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        var (statusCode, message) = exception switch
        {
            IDomainException domainException => (domainException.StatusCode, domainException.ErrorMessage),
            _ => (HttpStatusCode.InternalServerError, "an error occurred.")
        };

        return Problem(title: message, statusCode: (int)statusCode);
    }
}