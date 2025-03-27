using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerokuappTests.Pages
{
    public class DisappearingElementsPage
    {
        IPage _page;
        public DisappearingElementsPage(IPage page)
        {
            _page = page;
        }
        public ILocator NavigationElements => _page.Locator("ul li a");

        public async Task NavigateToPageAsync()
        {
            await _page.GotoAsync("https://the-internet.herokuapp.com/disappearing_elements");
        }

        public async Task<IReadOnlyList<ILocator>> GetNavigationElementsAsync()
        => await NavigationElements.AllAsync();

        public async Task<int> GetNavigationElementsCountAsync()
        {
            var elements = await GetNavigationElementsAsync();
            return elements.Count;
        }
        public async Task<bool> IsNavigationLinkPresentAsync(string elementName)
        {
            var gallery = _page.Locator("ul li a", new PageLocatorOptions { HasText = elementName });
            return await gallery.CountAsync() > 0;
        }

        public async Task ClickNavigationLinkAsync(string elementName)
        {
            var gallery = _page.Locator("ul li a", new PageLocatorOptions { HasText = elementName });
            await gallery.First.ClickAsync();
        }

        public async Task<string> GetCurrentUrlAsync() => _page.Url;

    }
}
