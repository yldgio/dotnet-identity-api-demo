using ErrorOr;

namespace Identity.Api.Application.Common.Errors;

public static partial class Errors
{
    public static class Login
    {
        public static Error UsernameNotFound => Error.NotFound(
            code: "Login.UsernameNotFound",
            description: "User not found");
        public static Error InvalidCredentials => Error.Validation(
            code: "Login.InvalidCredentials",
            description: "Invalid Credentials");
    }
}