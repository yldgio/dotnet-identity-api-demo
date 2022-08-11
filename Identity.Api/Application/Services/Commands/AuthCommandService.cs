using ErrorOr;

using Identity.Api.Application.Common.Errors;
using Identity.Api.Application.Common.Exceptions;
using Identity.Api.Application.Common.Interfaces;
using Identity.Api.Authentication.Common;
using Identity.Api.Domain.Entities;

namespace Identity.Api.Application.Services.Commands;

public interface IAuthCommandService
{
    ErrorOr<AuthenticationResult> Register(string FirstName,
                                           string LastName,
                                           string Username,
                                           string Password);
}

public class AuthCommandService : IAuthCommandService
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthCommandService(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string FirstName, string LastName, string Username, string Password)
    {
        // check if user exists
        if (_userRepository.GetUser(Username) is not null)
        {
            // throw new DuplicateUsernameException();
            return Errors.Registration.DuplicateUsername;
        }
        var user = new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Username = Username,
            Password = Password
        };
        // create the user (generate id) & persist
        _userRepository.AddUser(user);
        // create token
        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            User: user,
            Token: token);
    }
}
