namespace Identity.Api.Infrastructure.Config;
public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Secret { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    // "Secret":"super-secret-key",
    // "ExpiryMinutes": 60,
    // "Issuer": "myself",
    // "Audience":"myself"
}