using Identity.Api.Domain.Entities;

namespace Identity.Api.Application.Authentication.Common;

public record IdentityResult(
    User User);