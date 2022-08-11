using ErrorOr;

using Identity.Api.Authentication.Common;

using MediatR;
namespace Identity.Api.Authentication.Queries.Login;
public record LoginQuery(string Username, string Password) : IRequest<ErrorOr<AuthenticationResult>>;