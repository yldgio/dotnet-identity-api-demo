
using Microsoft.Extensions.Configuration;

namespace Identity.Api.Tests.Acceptance;

internal static class TestUtils
{
    public const string AppBaseUrlKey = "Identity.App:BaseUrl";
    public const string ComposeFileNamelKey = "DockerComposeFileName";
    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }
}
