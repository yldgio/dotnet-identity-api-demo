using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is null)
        {
            return;
        }
        context.Result = new ObjectResult(new { error = context.Exception.Message })
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
        context.ExceptionHandled = true;
    }
}