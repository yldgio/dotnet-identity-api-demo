using Bogus;
using Identity.Contracts.Auth;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Identity.Api.Tests.Integration;
public class IdentityApiBaseTest
{
    protected readonly HttpClient _httpClient;
    protected readonly Faker<RegisterRequest> _registerRequestFaker;
    public const string AValidPWD = "S0meValidpa$$wd";
    protected IdentityApiBaseTest()
    {
        var appFactory = new WebApplicationFactory<Program>();
        //.WithWebHostBuilder(builder => {
        //    builder.ConfigureServices(services => { });
        //});
        _httpClient = appFactory.CreateClient();
        
        _registerRequestFaker = new Faker<RegisterRequest>()
            .WithRecord()
            .RuleFor(r=>r.FirstName, f=>f.Name.FirstName())
            .RuleFor(r=>r.LastName, f=>f.Name.LastName())
            .RuleFor(r=>r.Username, f=>f.Person.UserName)
            .RuleFor(r=>r.Password, f=>AValidPWD);
    }
    protected async Task AuthenticateAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
            "bearer",
            await GetJwtTokenAsync());

    }
    protected async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var response = await SendRegisterAsync(request);
        return await response.Content.ReadFromJsonAsync<AuthResponse>();
    }
    protected async Task<HttpResponseMessage> SendRegisterAsync(RegisterRequest request)
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
                            AValidPWD)
                        )
            ).Token;
    }
}
