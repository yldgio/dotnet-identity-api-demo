using ErrorOr;

using Identity.Api.Application.Common.Errors;
using Identity.Api.Application.Common.Interfaces;
using Identity.Api.Authentication.Common;
using Identity.Api.Domain.Entities;

using MediatR;

namespace Identity.Api.Authentication.Queries.Login;

public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }


    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (_userRepository.GetUser(query.Username) is not User user)
        {
            return Errors.Login.UsernameNotFound;
        }

        // validate password
        if (user.Password != query.Password)
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
