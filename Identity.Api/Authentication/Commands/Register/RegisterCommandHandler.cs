using ErrorOr;

using Identity.Api.Application.Common.Errors;
using Identity.Api.Application.Common.Interfaces;
using Identity.Api.Authentication.Common;
using Identity.Api.Domain.Entities;

using MediatR;

namespace Identity.Api.Authentication.Commands.Register;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }


    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUser(command.Username) is not null)
        {
            // throw new DuplicateUsernameException();
            return await Task.FromResult(Errors.Registration.DuplicateUsername);
        }
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Username = command.Username,
            Password = command.Password
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
