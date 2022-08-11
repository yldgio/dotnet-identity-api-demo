using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Presentation.Controllers;
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}