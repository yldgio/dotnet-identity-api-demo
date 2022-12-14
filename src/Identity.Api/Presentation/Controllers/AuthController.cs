namespace Identity.Api.Presentation.Controllers;

using ErrorOr;

using Identity.Api.Application.Authentication.Commands.Register;
using Identity.Api.Application.Authentication.Common;
using Identity.Api.Application.Authentication.Queries.Login;
using Identity.Api.Application.Common.Errors;
using Identity.Contracts.Auth;

using MapsterMapper;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[AllowAnonymous]
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
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var result = await _mediator.Send(query, cancellationToken);
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
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        ErrorOr<AuthenticationResult> result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            authRresult => Ok(MapAuthResult(authRresult)),
            errors => Problem(errors)
        );
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
