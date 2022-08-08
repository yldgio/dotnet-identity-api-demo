namespace Identity.Contracts.Auth;

public record RegisterRequest(string FirstName,
                              string LastName,
                              string Username,
                              string Password);