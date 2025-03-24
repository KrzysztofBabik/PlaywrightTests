using HerokuappTests.Helpers;
using HerokuappTests.Pages;
using Microsoft.Playwright;
using Microsoft.VisualBasic;
using NUnit.Framework;

namespace HerokuappTests.Tests
{
    [TestFixture]
    [Category("Herokuapp")]
    public class ContextMenuTests
    {
        private PlaywrightSetup _playwright;
        private IBrowserContext _context;
        private IPage _page;
        private ContextMenuPage _contextMenuPage;

        [SetUp]
        public async Task SetUp()
        {
            _playwright = new PlaywrightSetup();
            await _playwright.InitializeAsync();
            _context = await _playwright.CreateNewContextAsync();
            _page = await _context.NewPageAsync();
            
            _contextMenuPage = new ContextMenuPage(_page);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _page.CloseAsync();
        }

        [Test]
        public async Task ContextMenu01_CheckJavaScriptAlert()
        {
            await _contextMenuPage.GoToUrlAsync();

            _page.Dialog += async (_, dialog) =>
            {
                Assert.That(dialog.Message, Is.EqualTo("You selected a context menu"));
                await dialog.AcceptAsync();
            };
            await _contextMenuPage.RighClickHotSpot();
        }
    }
}
