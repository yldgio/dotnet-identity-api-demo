using Identity.Api.Domain.Entities;

namespace Identity.Api.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);