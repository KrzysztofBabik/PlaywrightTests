using System.Threading.Tasks;
using Microsoft.Playwright;

namespace HerokuappTests.Pages
{
    public class ABTestPage
    {
        private readonly IPage _page;

        public ABTestPage(IPage page) => _page = page;

        public async Task<string> GetHeaderAsync()
        {
            return await _page.Locator("h3").TextContentAsync();
        }

        public async Task NavigateAsync()
        {
            await _page.GotoAsync("https://the-internet.herokuapp.com/abtest");
        }
    }
}
