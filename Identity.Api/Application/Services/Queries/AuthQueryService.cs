using ErrorOr;

using Identity.Api.Application.Common.Errors;
using Identity.Api.Application.Common.Exceptions;
using Identity.Api.Application.Common.Interfaces;
using Identity.Api.Authentication.Common;
using Identity.Api.Domain.Entities;

namespace Identity.Api.Application.Services.Queries;

public interface IAuthQueryService
{
    ErrorOr<AuthenticationResult> Login(string Username, string Password);
}

public class AuthQueryService : IAuthQueryService
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthQueryService(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string username, string password)
    {
        // user exists
        if (_userRepository.GetUser(username) is not User user)
        {
            return Errors.Login.UsernameNotFound;
        }

        // validate password
        if (user.Password != password)
        {
            return Errors.Login.InvalidCredentials;
        }

        // create token
        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            User: user,
            Token: token);
    }

}
