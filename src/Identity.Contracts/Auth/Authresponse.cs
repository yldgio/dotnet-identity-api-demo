namespace Identity.Contracts.Auth;

public record AuthResponse(Guid Id,
    string Username,
    string FirstName,
    string LastName,
    string Token);