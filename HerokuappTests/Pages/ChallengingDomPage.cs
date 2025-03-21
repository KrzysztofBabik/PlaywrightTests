using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    public class ChallengingDomPage
    {
        private readonly IPage _page;

        public ChallengingDomPage(IPage page)
        {
            _page = page;
        }

        public ILocator Button => _page.Locator("a.button").First;
        public ILocator ButtonAlert => _page.Locator("a.button.alert");
        public ILocator ButtonSuccess => _page.Locator("a.button.success");
        public ILocator TableRows => _page.Locator("tbody tr");

        public async Task NavigateToChallengingDomPageAsync()
        {
            await _page.GotoAsync("https://the-internet.herokuapp.com/challenging_dom");
        }

        public async Task ClickButtonAsync()
        {
            await Button.ClickAsync();
            Thread.Sleep(500);  // Bad approach
        }

        public async Task ClickButtonAlertAsync()
        {
            await ButtonAlert.ClickAsync();
            Thread.Sleep(500);  // Bad approach
        }
        public async Task ClickButtonSuccessAsync()
        {
            await ButtonSuccess.ClickAsync();
            Thread.Sleep(500);  // Bad approach
        }

        public async Task ClickDeleteRowAsync(int rowIndex)
        {
            var deleteButton = TableRows.Nth(rowIndex).Locator("a[href='#delete']");

            await deleteButton.ClickAsync();
        }

        public async Task ClickEditRowAsync(int rowIndex)
        {
            var deleteButton = TableRows.Nth(rowIndex).Locator("a[href='#edit']");

            await deleteButton.ClickAsync();
        }

        public async Task<bool> VerifyUrlAfterClickAsync(string keyWord)
        {
            var currentUrl = _page.Url;
            return currentUrl.Contains($"#{keyWord}") ? true : false;
        }


        public async Task<int> GetRowCountAsync()
        {
            return await TableRows.CountAsync();
        }

        public async Task<string> GetCellTextAsync(int rowIndex, int cellIndex)
        {
            var cell = TableRows.Nth(rowIndex).Locator($"td:nth-child({cellIndex})");
            return await cell.TextContentAsync();
        }

        public async Task<bool> GetCanvas()
        {
            var canvasLocator = _page.Locator("canvas");
            return await canvasLocator.CountAsync() > 0;
        }
    }
}
