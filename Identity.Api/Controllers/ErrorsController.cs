using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}