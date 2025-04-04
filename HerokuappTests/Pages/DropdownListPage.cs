using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerokuappTests.Pages
{
    public class DropdownListPage
    {
        private readonly IPage _page;
        public DropdownListPage(IPage page)
        {
            _page = page;
        }

        public ILocator Dropdown => _page.Locator("#dropdown");

        public async Task NavigateToPageAsync()
        {
            await _page.GotoAsync("https://the-internet.herokuapp.com/dropdown");
        }

        public async Task<string> GetSelectedOptionValueAsync()
        {
            return await Dropdown.InputValueAsync();
        }
        public async Task<string> GetSelectedOptionTextAsync()
        {
            var selectedValue = await GetSelectedOptionValueAsync();
            return await Dropdown.Locator($"option[value='{selectedValue}']").TextContentAsync();
        }

        public async Task<List<string>> GetAllDropdownOptionsAsync()
        {
            return (await Dropdown.Locator("option").AllTextContentsAsync()).ToList();
        }

        public async Task SelectDropdownOptionAsync(string optionText)
        {
            await Dropdown.SelectOptionAsync(new SelectOptionValue { Label = optionText });
        }
    }
}
