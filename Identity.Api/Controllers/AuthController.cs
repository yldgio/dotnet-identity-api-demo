namespace Identity.Api.Controllers;

using ErrorOr;

using Identity.Api.Application.Common.Errors;
using Identity.Api.Authentication.Commands.Register;
using Identity.Api.Authentication.Common;
using Identity.Api.Authentication.Queries.Login;
using Identity.Contracts.Auth;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class AuthController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public AuthController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var result = await _mediator.Send(query);
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
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        ErrorOr<AuthenticationResult> result = await _mediator.Send(command);

        return result.Match(
            authRresult => Ok(MapAuthResult(authRresult)),
            errors => Problem(errors)
        );
        // return Ok(MapAuthResult(result));
    }

    private AuthResponse MapAuthResult(AuthenticationResult result)
    {
        return _mapper.Map<AuthResponse>(result);
        // return new AuthResponse(
        //     Id: result.User.Id,
        //     Username: result.User.Username,
        //     FirstName: result.User.FirstName,
        //     LastName: result.User.LastName,
        //     Token: result.Token);
    }
}
