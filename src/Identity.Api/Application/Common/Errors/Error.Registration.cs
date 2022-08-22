using ErrorOr;

namespace Identity.Api.Application.Common.Errors;

public static partial class Errors
{
    public static class Registration
    {
        public static Error DuplicateUsername => Error.Conflict(
            code: "Registration.DuplicateUsername",
            description: "Username already exists");
    }
}