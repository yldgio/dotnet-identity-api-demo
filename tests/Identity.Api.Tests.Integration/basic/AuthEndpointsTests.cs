using FluentAssertions;
using System.Net;

namespace Identity.Api.Tests.Integration.basic;
public class AuthEndpointsTests : IdentityApiBaseTest
{
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