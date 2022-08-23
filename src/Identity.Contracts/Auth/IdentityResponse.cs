namespace Identity.Contracts.Auth;

public record IdentityResponse(Guid Id,
    string Username,
    string FirstName,
    string LastName);