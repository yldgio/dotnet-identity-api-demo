using Bogus;
using Identity.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Api.Tests.Integration;
public class TestUtils
{
    public const string AValidPWD = "S0meValidpa$$wd";
    public static Faker<RegisterRequest> ValidRagistrationFaker()
    {
        return new Faker<RegisterRequest>()
            .WithRecord()
            .RuleFor(r => r.FirstName, f => f.Name.FirstName())
            .RuleFor(r => r.LastName, f => f.Name.LastName())
            .RuleFor(r => r.Username, f => f.Person.UserName)
            .RuleFor(r => r.Password, f => AValidPWD);
    }

}
public class RegistrationHandler
{
    private readonly HttpClient _httpClient;

    public RegistrationHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task AuthenticateAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
            "bearer",
            await GetJwtTokenAsync());

    }
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var response = await SendRegisterAsync(request);
        return await response.Content.ReadFromJsonAsync<AuthResponse>();
    }
    public async Task<HttpResponseMessage> SendRegisterAsync(RegisterRequest request)
    {
        return await _httpClient.PostAsJsonAsync("/auth/register", request);
    }

    private async Task<string?> GetJwtTokenAsync()
    {
        return (await RegisterAsync(
                        new RegisterRequest(
                            "Any User",
                            "Test",
                            "any.user@test.com",
                            TestUtils.AValidPWD)
                        )
            ).Token;
    }

}
