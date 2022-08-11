using Identity.Api.Domain.Entities;

namespace Identity.Api.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);