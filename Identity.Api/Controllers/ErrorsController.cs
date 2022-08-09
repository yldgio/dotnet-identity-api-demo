using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        //how to access Error:
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        //custom status code: Problem(statusCode: 400)
        return Problem(detail: exception?.Message);

    }
}