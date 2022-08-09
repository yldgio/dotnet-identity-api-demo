namespace Identity.Api.Controllers;

using Identity.Api.Domain.Common.Errors;
using Identity.Api.Domain.Services;
using Identity.Contracts.Auth;

using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = _authService.Login(
            request.Username,
            request.Password);

        var response = MapAuthResult(result);
        return Ok(response);
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = _authService.Register(
            request.FirstName, request.LastName, request.Username,
            request.Password);
        return Ok(MapAuthResult(result));
    }

    private static AuthResponse MapAuthResult(AuthenticationResult result)
    {
        return new AuthResponse(
            Id: result.User.Id,
            Username: result.User.Username,
            FirstName: result.User.FirstName,
            LastName: result.User.LastName,
            Token: result.Token);
    }
}
