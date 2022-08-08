using Identity.Api.Domain.Entities;

namespace Identity.Api.Domain.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}