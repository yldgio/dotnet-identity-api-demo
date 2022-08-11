using ErrorOr;

using Identity.Api.Application.Authentication.Common;

using MediatR;
namespace Identity.Api.Application.Authentication.Queries.Login;
public record LoginQuery(string Username, string Password) : IRequest<ErrorOr<AuthenticationResult>>;