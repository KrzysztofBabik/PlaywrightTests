using System.Threading.Tasks;
using HerokuappTests.Helpers;
using HerokuappTests.Pages;
using Microsoft.Playwright;
using NUnit.Framework;

namespace HerokuappTests.Tests
{
    [TestFixture]
    [Category("Herokuapp")]
    public class AddElementPageTests
    {
        private PlaywrightSetup _playwrightSetup;
        private IBrowserContext _context;
        private IPage _page;
        private AddElementPage _addElementPage;

        [SetUp]
        public async Task Setup()
        {
            _playwrightSetup = new PlaywrightSetup();
            await _playwrightSetup.InitializeAsync();

            _context = await _playwrightSetup.CreateNewContextAsync();
            _page = await _context.NewPageAsync();
            _addElementPage = new AddElementPage(_page);

            await _addElementPage.NavigateAsync();
        }

        [Test]
        public async Task AddElement_ShouldIncreaseDeleteButtonCount()
        {
            int beforeCount = await _addElementPage.GetDeleteButtonCountAsync();
            await _addElementPage.ClickAddElementAsync();
            int afterCount = await _addElementPage.GetDeleteButtonCountAsync();

            Assert.That(afterCount, Is.EqualTo(beforeCount + 1), "The number of Delete buttons has not increased");
        }

        [Test]
        public async Task DeleteButton_ShouldRemoveElement()
        {
            await _addElementPage.ClickAddElementAsync();
            int beforeCount = await _addElementPage.GetDeleteButtonCountAsync();

            await _addElementPage.ClickDeleteButtonAsync();
            int afterCount = await _addElementPage.GetDeleteButtonCountAsync();

            Assert.That(afterCount, Is.EqualTo(beforeCount - 1), "The Delete button did not delete the item");
        }

        [TearDown]
        public async Task TearDown()
        {
            await _playwrightSetup.DisposeAsync();
        }
    }
}
