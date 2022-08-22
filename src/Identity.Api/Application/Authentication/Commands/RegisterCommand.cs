using ErrorOr;

using Identity.Api.Application.Authentication.Common;

using MediatR;

namespace Identity.Api.Application.Authentication.Commands;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Username,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;