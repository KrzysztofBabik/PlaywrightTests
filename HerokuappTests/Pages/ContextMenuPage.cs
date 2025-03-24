using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerokuappTests.Pages
{
    public class ContextMenuPage
    {
        private IPage _page;
        public ContextMenuPage(IPage page)
        {
            _page = page;
        }

        ILocator HotSpot => _page.Locator("#hot-spot");

        public async Task GoToUrlAsync()
        {
            await _page.GotoAsync("https://the-internet.herokuapp.com/context_menu");
        }
        public async Task RighClickHotSpot()
        {
            await HotSpot.ClickAsync(new() { Button = MouseButton.Right });
        }
    }
}
