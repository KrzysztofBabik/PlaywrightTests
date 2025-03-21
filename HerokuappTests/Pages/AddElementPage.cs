using System.Threading.Tasks;
using Microsoft.Playwright;

namespace HerokuappTests.Pages
{
    public class AddElementPage
    {
        private readonly IPage _page;
        private readonly ILocator _addButton;
        private readonly ILocator _deleteButtons;

        public AddElementPage(IPage page)
        {
            _page = page;
            _addButton = _page.Locator("button[onclick='addElement()']");
            _deleteButtons = _page.Locator("button.added-manually");
        }

        public async Task NavigateAsync()
        {
            await _page.GotoAsync("https://the-internet.herokuapp.com/add_remove_elements/");
        }

        public async Task ClickAddElementAsync()
        {
            await _addButton.ClickAsync();
        }

        public async Task<int> GetDeleteButtonCountAsync()
        {
            return await _deleteButtons.CountAsync();
        }

        public async Task ClickDeleteButtonAsync()
        {
            if (await _deleteButtons.CountAsync() > 0)
            {
                await _deleteButtons.First.ClickAsync();
            }
        }
    }
}
