using HerokuappTests.Helpers;
using HerokuappTests.Pages;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HerokuappTests.Tests
{
    [TestFixture]
    [Category("Herokuapp")]
    public class CheckboxesTests
    {
        private PlaywrightSetup _playwright;
        private IBrowserContext _context;
        private IPage _page;
        private CheckboxesPage _checkboxesPage; 

        [SetUp]
        public async Task SetUp()
        {
            _playwright = new PlaywrightSetup();
            await _playwright.InitializeAsync();
            _context = await _playwright.CreateNewContextAsync();
            _page = await _context.NewPageAsync();

            _checkboxesPage = new CheckboxesPage(_page);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _page.CloseAsync();
        }

        [Test]
        public async Task Checkboxes01_CheckboxCount()
        {
            await _checkboxesPage.NavigateToUrlAsync();

            var counter = await _checkboxesPage.GetCheckboxesCount();
            Assert.That(2, Is.EqualTo(counter), "Number of checkboxes are wrong");
        }

        [Test]
        public async Task Checkboxes02_ChangeCheckboxState()
        {
            await _checkboxesPage.NavigateToUrlAsync();

            int checkboxCounter = await _checkboxesPage.GetCheckboxesCount();
            for (int idx = 0; idx < checkboxCounter; idx++)
            {
                var initState = await _checkboxesPage.GetCheckboxState(idx);
                await _checkboxesPage.ChangeCheckboxState(idx);
                var newState = await _checkboxesPage.GetCheckboxState(idx);

                Assert.That(initState, Is.Not.EqualTo(newState));
            }
        }
    }
}
