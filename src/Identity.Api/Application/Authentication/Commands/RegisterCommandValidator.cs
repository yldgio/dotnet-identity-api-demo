using System.Text.RegularExpressions;

using FluentValidation;

using Identity.Api.Application.Authentication.Commands;

namespace Identity.Api.Application.Authentication.Commands;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).Length(3, 255);
        RuleFor(x => x.LastName).Length(3, 255);
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .Length(8, 35)
            .WithMessage("{PropertyName} is required.")
            .Must(x => BeValidPassword(x))
            .WithMessage("{PropertyName} must contain at least 1 symbol, 1 number, 1 upper-case char and 1 lower-case char"); ;
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.");
    }
    private bool BeValidPassword(string pw)
    {
        var lowercase = new Regex("[a-z]+");
        var uppercase = new Regex("[A-Z]+");
        var digit = new Regex("(\\d)+");
        var symbol = new Regex("(\\W)+");

        return (lowercase.IsMatch(pw) && uppercase.IsMatch(pw) && digit.IsMatch(pw) && symbol.IsMatch(pw));
    }
}