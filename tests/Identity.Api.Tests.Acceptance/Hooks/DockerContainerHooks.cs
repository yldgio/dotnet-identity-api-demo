using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Builders;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using BoDi;

namespace Identity.Api.Tests.Acceptance.Hooks;

[Binding]
public sealed class DockerContainerHooks
{
    // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
    private static ICompositeService _compositeService;
    private readonly IObjectContainer _container;

    public DockerContainerHooks(IObjectContainer container)
    {
        _container = container;
    }

    [BeforeTestRun]
    public static void DockerComposeUp()
    {
        var config = TestUtils.LoadConfiguration();
        var composeFilename = config[TestUtils.ComposeFileNamelKey];
        var composeFilePath = GetDockerComposeLocation(composeFilename);
        var baseUrl = config[TestUtils.AppBaseUrlKey];
        _compositeService = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(composeFilePath)
            .RemoveOrphans()
            .WaitForHttp("webapi",$"{baseUrl}/healthz", 
                continuation: (response, _)=> response.Code != System.Net.HttpStatusCode.OK ? 2000 : 0)
            .Build()
            .Start();
    }
    [AfterTestRun]
    public static void DockerComposeDown()
    {
        _compositeService.Stop();
        _compositeService.Dispose();

    }
    [BeforeScenario]
    public void RegisterHttpClient()
    {
        var config = TestUtils.LoadConfiguration();
        var baseUrl = config[TestUtils.AppBaseUrlKey];
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
        _container.RegisterInstanceAs(httpClient);
    }
    private static string GetDockerComposeLocation(string filename)
    {
        var dir = Directory.GetCurrentDirectory();
        while(!Directory.EnumerateFiles(dir, "*.yml").Any(f => f.EndsWith(filename)))
        {
            dir = dir.Substring(0, dir.LastIndexOf(Path.DirectorySeparatorChar));
        }
        return Path.Combine(dir, filename);
    }


}