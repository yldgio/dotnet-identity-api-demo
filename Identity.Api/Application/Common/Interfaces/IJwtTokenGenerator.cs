using Identity.Api.Domain.Entities;

namespace Identity.Api.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}