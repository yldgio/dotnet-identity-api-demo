using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Builders;
using Microsoft.Extensions.Configuration;

using TechTalk.SpecFlow;
using BoDi;
using Microsoft.Playwright;
using BlazorApp.Tests.Acceprance.PageObjects;

namespace BlazorApp.Tests.Acceprance.Hooks
{
    [Binding]
    public sealed class UISpecHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private static ICompositeService _compositeService;
        private readonly IObjectContainer _container;

        public UISpecHooks(IObjectContainer container)
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
                .WaitForHttp("blazorapp",$"{baseUrl}/", 
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
        [BeforeScenario("@Counter")]
        public async Task BeforeCounterScenario()
        {
            // Example of filtering hooks using tags. (in this case, this 'before scenario' hook will execute if the feature/scenario contains the tag '@tag1')
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=hooks#tag-scoping

            var config = TestUtils.LoadConfiguration();
            var playwright = await Playwright.CreateAsync(); //create the driver
            //var browser = await playwright.Chromium.LaunchAsync(); //launch browser headless
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                //Headless = false,
                SlowMo = 500
            });
            var context = await browser.NewContextAsync();
            var baseUrl = config[TestUtils.AppBaseUrlKey];
            var pageObject = new CounterPageObject(context, baseUrl);
            _container.RegisterInstanceAs(playwright);
            _container.RegisterInstanceAs(browser);
            _container.RegisterInstanceAs(pageObject);

        }

        [AfterScenario("@Counter")]
        public async Task AfterScenario()
        {
            var browser = _container.Resolve<IBrowser>();
            await browser.CloseAsync();
            var playwright = _container.Resolve<IPlaywright>();
            playwright.Dispose();
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
}