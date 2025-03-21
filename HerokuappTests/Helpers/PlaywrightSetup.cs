using System.Threading.Tasks;
using Microsoft.Playwright;

namespace HerokuappTests.Helpers
{
    public class PlaywrightSetup
    {
        public IPlaywright Playwright { get; private set; }
        public IBrowser Browser { get; private set; }

        public async Task InitializeAsync()
        {
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
        }

        public async Task<IBrowserContext> CreateNewContextAsync()
        {
            return await Browser.NewContextAsync();
        }

        public async Task DisposeAsync()
        {
            await Browser.CloseAsync();
            Playwright.Dispose();
        }
    }
}
