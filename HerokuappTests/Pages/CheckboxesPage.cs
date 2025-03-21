using Microsoft.Playwright;

namespace HerokuappTests.Pages
{
    public class CheckboxesPage
    {
        public readonly IPage _page;
        public readonly ILocator Checkbox;
        public CheckboxesPage(IPage page)
        {
            _page = page;
            Checkbox = _page.Locator("input[type='checkbox']");
        }

        public async Task NavigateToUrlAsync()
        {
            await _page.GotoAsync("https://the-internet.herokuapp.com/checkboxes");
        }

        public async Task<int> GetCheckboxesCount()
        {
            return await Checkbox.CountAsync();
        }
        public async Task<bool> GetCheckboxState(int idx)
        {
            bool isChecked = await Checkbox.Nth(idx).IsCheckedAsync();
            return isChecked;
        }

        public async Task ChangeCheckboxState(int idx)
        {
            await Checkbox.Nth(idx).ClickAsync();
        }
    }
}
