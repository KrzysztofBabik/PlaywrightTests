using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using HerokuappTests.Helpers;
using HerokuappTests.Pages;
using Microsoft.Playwright;
using NUnit.Framework;

namespace HerokuappTests.Tests
{
    [TestFixture]
    [Category("Herokuapp")]
    public class BasicAuthPageTests
    {
        private PlaywrightSetup _playwrightSetup;
        private IBrowserContext _context;
        private IPage _page;
        private BasicAuthPage _basicAuthPage;

        [SetUp]
        public async Task Setup()
        {
            _playwrightSetup = new PlaywrightSetup();
            await _playwrightSetup.InitializeAsync();

            _context = await _playwrightSetup.CreateNewContextAsync();
            _page = await _context.NewPageAsync();
            _basicAuthPage = new BasicAuthPage(_page);
        }

        [Test]
        public async Task BasicAuth01_ValidCredentials()
        {
            await _basicAuthPage.SendCorrectCredentials();
            var pageText = await _basicAuthPage.GetPageTextAsync();
            Assert.That(pageText, Does.Contain("Congratulations! You must have the proper credentials."), "No message about correct login.");
        }

        [TearDown]
        public async Task TearDown()
        {
            await _playwrightSetup.DisposeAsync();
        }
    }
}
