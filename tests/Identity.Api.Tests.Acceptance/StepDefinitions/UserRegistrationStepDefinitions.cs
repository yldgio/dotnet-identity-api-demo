using System;
using System.Net.Http.Json;

using FluentAssertions;

using Identity.Contracts.Auth;

using System.Text.Json;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Identity.Api.Tests.Acceptance.StepDefinitions
{
    public record ErrorResponse (string Type, string Title, int Status);

    [Binding]
    public class UserRegistrationStepDefinitions
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _context;
        private readonly JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public UserRegistrationStepDefinitions(HttpClient httpClient, ScenarioContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        [When(@"I request to register a new user with the following correct data")]
        public async Task WhenIRequestToRegisterANewUserWithTheFollowingCorrectData(Table table)
        {
            var registrationRequests = table.CreateSet<RegisterRequest>();
            //var registrationResponses = new List<AuthResponse>();
            foreach (var regRequest in registrationRequests)
            {
                var response = await _httpClient.PostAsJsonAsync<RegisterRequest>("auth/register", regRequest, serializeOptions);
                response.IsSuccessStatusCode.Should().BeTrue();
                var registrationResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                registrationResponse.Token.Should().NotBeNull();
                //registrationResponses.Add(registrationResponse);
            }
            _context.Add("RegistrationRequests", registrationRequests.ToList());
        }
        [Then(@"A user should be able to login with the following credential")]
        public async Task ThenAUserShouldBeAbleToLoginWithTheFollowingCredential(Table table)
        {
            var loginRequests = table.CreateSet<LoginRequest>();
            foreach (var loginRequest in loginRequests)
            {
                var response = await _httpClient.PostAsJsonAsync("auth/login", loginRequest, serializeOptions);
                response.IsSuccessStatusCode.Should().BeTrue();
                var loginResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                loginResponse.Token.Should().NotBeNull();
            }
        }

        [Given(@"the following user in the system")]
        public async Task GivenTheFollowingUserInTheSystem(Table table)
        {
            var registrationRequests = table.CreateSet<RegisterRequest>();
            foreach (var regRequest in registrationRequests)
            {
                var response = await _httpClient.PostAsJsonAsync("auth/register", regRequest, serializeOptions);
                //response.IsSuccessStatusCode.Should().BeTrue();
            }
        }

        [When(@"I request to register a new user with the following data")]
        public async Task WhenIRequestToRegisterANewUserWithTheFollowingData(Table table)
        {
            var registrationRequests = table.CreateSet<RegisterRequest>();
            var registrationResponses = new List<ErrorResponse>();
            foreach (var regRequest in registrationRequests)
            {
                var response = await _httpClient.PostAsJsonAsync("auth/register", regRequest, serializeOptions);
                response.IsSuccessStatusCode.Should().BeFalse();
                var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                registrationResponses.Add(error);
            }
            _context.Add("RegistrationResponses", registrationResponses.ToList());
        }

        [Then(@"The user is not created and the response contains the error ""([^""]*)""")]
        public void ThenTheUserIsNotCreatedAndTheResponseContainsTheError(string error)
        {
            var registrationResponses = _context.Get<List<ErrorResponse>>("RegistrationResponses");
            registrationResponses.Select(res=> res.Title).Should().AllBe(error);
        }
    }
}
