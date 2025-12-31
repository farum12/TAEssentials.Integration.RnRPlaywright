using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Reqnroll;
using TAEssentials.UI.DataClasses.Configuration;
using TAEssentials.UI.PageObjects.Components;
using TAEssentials.UI.Utils;

namespace TAEssentials.UI.Hooks
{
    [Binding]
    public class GlobalHooks
    {
        private static Reqnroll.BoDi.IObjectContainer SharedContainer;
        private static Configuration Configuration;

        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;
        private readonly IReqnrollOutputHelper _reqnrollOutputHelper;

        public GlobalHooks(FeatureContext featureContext, ScenarioContext scenarioContext, IReqnrollOutputHelper reqnrollOutputHelper)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
            _reqnrollOutputHelper = reqnrollOutputHelper;
        }

        [BeforeTestRun(Order = 0)]
        public static async Task GetAdminToken()
        {
            await TokenProvider.RetrieveAndStoreAdminTokenAsync();
        }

        [BeforeTestRun(Order = 1)]
        public static async Task BeroreTestRun(Reqnroll.BoDi.IObjectContainer container)
        {
            File.Delete("state.json");
            SharedContainer = container;

            Configuration = ConfigurationProvider.GetConfigurationFile<Configuration>("appsettings.json", "Configuration");
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync(new Microsoft.Playwright.BrowserTypeLaunchOptions
            {
                Headless = Configuration.BrowserSettings?.Headless,
                Channel = Configuration.BrowserSettings?.Channel,
                SlowMo = Configuration.BrowserSettings?.SlowMo
            });

            SharedContainer.RegisterInstanceAs(browser);
        }

        [AfterTestRun]
        public static async Task AfterTestRun()
        {
            await SharedContainer.Resolve<IBrowser>().CloseAsync();
            File.Delete("state.json");
        }

        [BeforeScenario(Order = 1)]
        public void RegisterComponents(Reqnroll.BoDi.IObjectContainer objectContainer)
        {
            //objectContainer.RegisterTypeAs<ClassName, InterFaceName>().InstancePerDependency();
            // Default type registration is Single Instannce;
            // If we deal with more than with 1 Component per page we should register them as InstancePerDependency
            // Example:
            //objectContainer.RegisterTypeAs<IDropdown, Dropdown>().InstancePerDependency();

            // We need to have Interface for each Component to be able to register them as InstancePerDependency
            // In PO:
            // public IDropdown MyDropdown { get; private set; }
            // In Constructor:
            // MyDropdown = objectContainer.Resolve<IDropdown>().SetDropdownLocator("locator or ILocator");
            // MyDropdown = myDropdown.SetDropdownLocator("locator or ILocator");
        }

        [BeforeScenario(Order = 1)]
        public void RegisterFactories(Reqnroll.BoDi.IObjectContainer objectContainer)
        {
            objectContainer.RegisterInstanceAs<Func<ILocator, ProductCard>>(loc => new ProductCard(loc));
            objectContainer.RegisterInstanceAs<Func<ILocator, ProductReview>>(loc => new ProductReview(loc));
            objectContainer.RegisterInstanceAs<Func<ILocator, WishlistItem>>(loc => new WishlistItem(loc));
        }

        [BeforeScenario(Order = 2)]
        public async Task ScenarioSetupAsync(Reqnroll.BoDi.IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            var browser = SharedContainer.Resolve<IBrowser>();

            var statePath = (File.Exists("state.json")) ? "state.json" : null;

            var options = new BrowserNewContextOptions() { Locale = "en-US", StorageStatePath = statePath };
            var context = await browser.NewContextAsync(options);
            var page = await context.NewPageAsync();

            page.SetDefaultTimeout(Configuration!.BrowserSettings!.DefaultExpectedTimeout);
            Assertions.SetDefaultExpectTimeout(Configuration!.BrowserSettings!.DefaultExpectedTimeout);

            objectContainer.RegisterInstanceAs(page);

        }

        [BeforeScenario(Order = 3)]
        public async Task StartTracingAsync(IPage page)
        {
            var configuration = ConfigurationProvider.GetConfiguration();

            if (configuration.TraceSettings != null && configuration.TraceSettings.EnableTracing)
            {
                await page.Context.Tracing.StartAsync(new TracingStartOptions
                {
                    Title = _scenarioContext.ScenarioInfo.Title,
                    Screenshots = configuration.TraceSettings?.Screenshots ?? false,
                    Snapshots = configuration.TraceSettings?.Snapshots ?? false,
                    Sources = configuration.TraceSettings?.Sources ?? false
                });
            }
        }

        [AfterScenario(Order=1)]
        public async Task TearDownAsync(Reqnroll.BoDi.IObjectContainer objectContainer)
        {
            var page = objectContainer.Resolve<IPage>();

            await ScreenshotAsync(page);
            await page.CloseAsync();
        }

        [AfterScenario(Order=2)]
        public async Task StopTracingAsync(IPage page)
        {
            var browser = SharedContainer.Resolve<IBrowser>();
            var configuration = ConfigurationProvider.GetConfiguration();

            if (!configuration.TraceSettings!.EnableTracing)
            {
                return;
            }

            var isFailure = !_scenarioContext.ScenarioExecutionStatus.Equals(ScenarioExecutionStatus.OK) &&
                            !_scenarioContext.ScenarioExecutionStatus.Equals(ScenarioExecutionStatus.StepDefinitionPending) &&
                            !_scenarioContext.ScenarioExecutionStatus.Equals(ScenarioExecutionStatus.Skipped);

            if (isFailure)
            {
                var rgx = new Regex("[^a-zA-Z0-9]");
                var myTI = new CultureInfo("en-US", false).TextInfo;

                var scenarioTitleToTitleCase = myTI.ToTitleCase(_scenarioContext.ScenarioInfo.Title).Replace(" ", "");
                var scenarioTitleNormalized = rgx.Replace(scenarioTitleToTitleCase, "");
                var tracePath = Path.Combine(Directory.GetCurrentDirectory(), "playwright-traces", $"Trace_{scenarioTitleNormalized}.zip");
                await page.Context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = tracePath
                });
                TestContext.AddTestAttachment(tracePath);
            } else 
            {
                await page.Context.Tracing.StopAsync();
            }
        }

        private async Task ScreenshotAsync(IPage page)
        {
            if (_scenarioContext.ScenarioExecutionStatus.Equals(ScenarioExecutionStatus.OK) ||
                _scenarioContext.ScenarioExecutionStatus.Equals(ScenarioExecutionStatus.StepDefinitionPending) ||
                _scenarioContext.ScenarioExecutionStatus.Equals(ScenarioExecutionStatus.Skipped))
            {
                return;
            }

            var screenshotPath = $"{Directory.GetCurrentDirectory()}/00_{_scenarioContext.ScenarioInfo.Title}.png";

            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = screenshotPath,
                FullPage = true
            });

            TestContext.AddTestAttachment(screenshotPath);
        }
    }
}