using ErrorOr;

using Identity.Api.Authentication.Common;

using MediatR;

namespace Identity.Api.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Username,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;