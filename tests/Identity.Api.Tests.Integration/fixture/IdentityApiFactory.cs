using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
namespace Identity.Api.Tests.Integration.fixture;

/// <summary>
/// Alternative initialization with fixture:
/// shared instance per Test Class
/// </summary>
public class IdentityApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        /// <summary>
        /// example of overriding services or services configuration
        /* builder.ConfigureTestServices(services =>
        {
           services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "api1";
                });
        });*/
    }
}
