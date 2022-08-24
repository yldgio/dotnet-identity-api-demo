using FluentAssertions;
using System.Net;

namespace Identity.Api.Tests.Integration;
public class AuthEndpointsTests : IdentityApiBaseTest
{
    [Fact]
    public async Task Register_WhenRequestIsValid_ShouldReturnAnAuthResponse()
    {

        // Arrange
        var request = _registerRequestFaker.Generate();

        // Act
        var response = await RegisterAsync(request);

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
        await SendRegisterAsync(request);
        // Act
        var dupResponse = await SendRegisterAsync(request);
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
        var response = await SendRegisterAsync(request);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

}