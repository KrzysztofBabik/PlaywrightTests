using NUnit.Framework;
using PlaywrightTests.Pages;
using Microsoft.Playwright;
using HerokuappTests.Helpers;

namespace PlaywrightTests
{
    [TestFixture]
    [Category("Herokuapp")]
    public class ChallengingDomTests
    {
        private PlaywrightSetup _playwrightSetup;
        private IPage _page;
        private IBrowserContext _context;
        private ChallengingDomPage _challengingDomPage;

        [SetUp]
        public async Task SetUp()
        {
            _playwrightSetup = new PlaywrightSetup();
            await _playwrightSetup.InitializeAsync();

            _context = await _playwrightSetup.CreateNewContextAsync();
            _page = await _context.NewPageAsync();
            _challengingDomPage = new ChallengingDomPage(_page);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _playwrightSetup.DisposeAsync();
        }

        [Test]
        public async Task ChallengingDOM01_RowCount()
        {
            await _challengingDomPage.NavigateToChallengingDomPageAsync();

            var initialRowCount = await _challengingDomPage.GetRowCountAsync();
            Assert.AreEqual(10, initialRowCount);
        }

        [Test]
        public async Task ChallengingDOM02_ClickDeleteRow()
        {
            await _challengingDomPage.NavigateToChallengingDomPageAsync();

            var initialRowCount = await _challengingDomPage.GetRowCountAsync();
            if (initialRowCount < 1) Assert.Fail("No rows found!");

            await _challengingDomPage.ClickDeleteRowAsync(0);

            bool isUrlContainsKeyWord = await _challengingDomPage.VerifyUrlAfterClickAsync("delete");
            Assert.IsTrue(isUrlContainsKeyWord, "Delete button does not work or cannot be found");
        }

        [Test]
        public async Task ChallengingDOM03_ClickEditRow()
        {
            await _challengingDomPage.NavigateToChallengingDomPageAsync();

            var initialRowCount = await _challengingDomPage.GetRowCountAsync();
            if (initialRowCount < 1) Assert.Fail("No rows found!");

            await _challengingDomPage.ClickEditRowAsync(0);

            bool isUrlContainsKeyWord = await _challengingDomPage.VerifyUrlAfterClickAsync("edit");
            Assert.IsTrue(isUrlContainsKeyWord, "Delete button does not work or cannot be found");
        }

        [Test]
        public async Task ChallengingDOM04_CellTextMatchExpectedValues()
        {
            await _challengingDomPage.NavigateToChallengingDomPageAsync();

            var cellText = await _challengingDomPage.GetCellTextAsync(0, 1);
            Assert.AreEqual("Iuvaret0", cellText);

            cellText = await _challengingDomPage.GetCellTextAsync(0, 2);
            Assert.AreEqual("Apeirian0", cellText);
        }

        [Test]
        public async Task ChallengingDOM05_LocateButtonsByClass()
        {
            await _challengingDomPage.NavigateToChallengingDomPageAsync();

            string initText = await _challengingDomPage.Button.InnerTextAsync();
            await _challengingDomPage.ClickButtonAsync();
            string newText = await _challengingDomPage.Button.InnerTextAsync();

            Assert.That(initText, Is.Not.EqualTo(newText));

            initText = await _challengingDomPage.ButtonAlert.InnerTextAsync();
            await _challengingDomPage.ClickButtonAlertAsync();
            initText = await _challengingDomPage.ButtonAlert.InnerTextAsync();

            Assert.That(initText, Is.Not.EqualTo(newText));

            initText = await _challengingDomPage.ButtonSuccess.InnerTextAsync();
            await _challengingDomPage.ClickButtonSuccessAsync();
            initText = await _challengingDomPage.ButtonSuccess.InnerTextAsync();

            Assert.That(initText, Is.Not.EqualTo(newText));
        }
        [Test]
        public async Task ChallengingDOM06_LocateCanvas()
        {
            await _challengingDomPage.NavigateToChallengingDomPageAsync();
            bool isCanvasExist = await _challengingDomPage.GetCanvas();

            Assert.True(isCanvasExist, "Canva does not exist");
        }
    }
}
