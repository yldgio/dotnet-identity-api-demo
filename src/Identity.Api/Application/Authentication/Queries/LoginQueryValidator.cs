using FluentValidation;

using Identity.Api.Application.Authentication.Queries.Login;

namespace Identity.Api.Application.Authentication.Queries;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
    }
}