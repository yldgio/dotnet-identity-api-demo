namespace Identity.Api.Controllers;

using ErrorOr;

using Identity.Api.Domain.Common.Errors;
using Identity.Api.Domain.Services;
using Identity.Contracts.Auth;

using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class AuthController : ApiController
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
        if (result.IsError && result.FirstError == Errors.Login.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                           title: result.FirstError.Description);
        }
        return result.Match(
            authRresult => Ok(MapAuthResult(authRresult)),
            errors => Problem(errors)
        );
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> result = _authService.Register(
            request.FirstName, request.LastName, request.Username,
            request.Password);

        return result.Match(
            authRresult => Ok(MapAuthResult(authRresult)),
            errors => Problem(errors)
        );
        // return Ok(MapAuthResult(result));
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
