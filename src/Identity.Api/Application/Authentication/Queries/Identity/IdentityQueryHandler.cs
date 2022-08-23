using ErrorOr;
using FluentValidation;
using Identity.Api.Application.Authentication.Common;
using Identity.Api.Application.Common.Errors;
using Identity.Api.Application.Common.Interfaces;
using Identity.Api.Domain.Entities;
using MediatR;
namespace Identity.Api.Application.Authentication.Queries.Identity;
public record IdentityQueryGet(string Username) : IRequest<ErrorOr<IdentityResult>>;
public class IdentityQueryGetValidator : AbstractValidator<IdentityQueryGet>
{
    public IdentityQueryGetValidator() => RuleFor(x => x.Username).NotEmpty();
}
public class IdentityQueryGetHandler :
    IRequestHandler<IdentityQueryGet, ErrorOr<IdentityResult>>
{
    private readonly IUserRepository _userRepository;

    public IdentityQueryGetHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<IdentityResult>> Handle(IdentityQueryGet query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (_userRepository.GetUser(query.Username) is not User user)
        {
            return Errors.Login.UsernameNotFound;
        }

        return new IdentityResult(
            User: user);
    }
}