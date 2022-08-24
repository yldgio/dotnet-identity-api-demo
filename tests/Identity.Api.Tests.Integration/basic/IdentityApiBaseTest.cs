using Bogus;
using Identity.Contracts.Auth;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Identity.Api.Tests.Integration.basic;
/// <summary>
/// Simple initialization of factory:
/// one instance per test method
/// </summary>

public class IdentityApiBaseTest
{
    protected readonly HttpClient _httpClient;
    protected readonly Faker<RegisterRequest> _registerRequestFaker;
    protected readonly RegistrationHandler _registrationHandler;

    protected IdentityApiBaseTest()
    {
        var appFactory = new WebApplicationFactory<Program>();
        //.WithWebHostBuilder(builder => {
        //    builder.ConfigureServices(services => { });
        //});
        _httpClient = appFactory.CreateClient();

        _registerRequestFaker = TestUtils.ValidRagistrationFaker();
        _registrationHandler = new RegistrationHandler(_httpClient);
    }

}
