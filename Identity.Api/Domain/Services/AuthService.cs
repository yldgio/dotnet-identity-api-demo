using System.Net;

using Identity.Api.Domain.Common.Errors;
using Identity.Api.Domain.Common.Interfaces;
using Identity.Api.Domain.Entities;

namespace Identity.Api.Domain.Services;

public interface IAuthService
{
    AuthenticationResult Login(string Username, string Password);
    AuthenticationResult Register(string FirstName, string LastName, string Username, string Password);
}

public class AuthService : IAuthService
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthService(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string username, string password)
    {
        // user exists
        if (_userRepository.GetUser(username) is not User user)
        {
            throw new Exception("User not found");
        }

        // validate password
        if (user.Password != password)
        {
            throw new Exception("Invalid Password");
        }

        // create token
        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            User: user,
            Token: token);
    }

    public AuthenticationResult Register(string FirstName, string LastName, string Username, string Password)
    {
        // check if user exists
        if (_userRepository.GetUser(Username) is not null)
        {
            throw new DuplicateUsernameException();
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
