using System.Threading.Tasks;
using Microsoft.Playwright;

namespace HerokuappTests.Pages
{
    public class BasicAuthPage
    {
        private readonly IPage _page;

        public BasicAuthPage(IPage page)
        {
            _page = page;
        }

        public async Task SendCorrectCredentials()
        {
            var url = "https://admin:admin@the-internet.herokuapp.com/basic_auth";
            await _page.GotoAsync(url);
        }

        public async Task<string> GetPageTextAsync()
        {
            var textElement = _page.Locator("div.example > p");
            return await textElement.TextContentAsync();
        }
    }
}
