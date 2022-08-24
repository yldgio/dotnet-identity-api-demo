using Bogus;
using FluentAssertions;
using Identity.Contracts.Auth;
using System.Net;

namespace Identity.Api.Tests.Integration.fixture;
public class AuthEndpointsWithFixtureTests : IClassFixture<IdentityApiFactory>
{
    protected readonly Faker<RegisterRequest> _registerRequestFaker = TestUtils.ValidRagistrationFaker();
    private readonly RegistrationHandler _registrationHandler;

    public AuthEndpointsWithFixtureTests(IdentityApiFactory factory)
    {
        _registrationHandler = new RegistrationHandler(factory.CreateClient());
    }

    [Fact]
    public async Task Register_WhenRequestIsValid_ShouldReturnAnAuthResponse()
    {

        // Arrange
        var request = _registerRequestFaker.Generate();

        // Act
        var response = await _registrationHandler.RegisterAsync(request);

        // Assert
        response.Should().NotBeNull();
        response.Token.Should().NotBeNullOrEmpty();
        response.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Register_WhenUsernameIsDuplicate_ShouldReturnConflict()
    {
        // Arrange
        var request = _registerRequestFaker
            .Generate();
        await _registrationHandler.SendRegisterAsync(request);
        // Act
        var dupResponse = await _registrationHandler.SendRegisterAsync(request);
        // Assert
        dupResponse.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }


    [Fact]
    public async Task Register_WhenUsernameIsDuplicate_ShouldReturnError()
    {
        // Arrange
        var request = _registerRequestFaker
            .WithRecord()
            .RuleFor(r => r.Username, f => string.Empty)
            .Generate();
        // Act
        var response = await _registrationHandler.SendRegisterAsync(request);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

}