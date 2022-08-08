using Identity.Api.Domain.Entities;

namespace Identity.Api.Domain.Services;

public record AuthenticationResult(
    User User,
    string Token);