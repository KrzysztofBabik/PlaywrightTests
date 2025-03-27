using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerokuappTests.Pages
{
    public class DragAndDropPage
    {
        IPage _page;
        public DragAndDropPage(IPage page)
        {
            _page = page;
        }

        public ILocator ColumnA => _page.Locator("#column-a");
        public ILocator ColumnB => _page.Locator("#column-b");

        public async Task NavigateToPageAsyc()
        {
            await _page.GotoAsync("https://the-internet.herokuapp.com/drag_and_drop");
        }

        public async Task DragAAndDropOnB()
        {
            await ColumnA.DragToAsync(ColumnB);
        }

        public async Task<string> GetTextFromColumnA()
        {
            return await ColumnA.TextContentAsync();
        }

        public async Task<string> GetTextFromColumnB()
        {
            return await ColumnB.TextContentAsync();

        }
    }
}
