﻿using System.Threading.Tasks;
using HerokuappTests.Helpers;
using HerokuappTests.Pages;
using Microsoft.Playwright;
using NUnit.Framework;

namespace HerokuappTests.Tests
{
    [TestFixture]
    [Category("Herokuapp")]
    public class ABTestPageTests
    {
        private PlaywrightSetup _playwrightSetup;
        private IBrowserContext _context;
        private IPage _page;
        private ABTestPage _abTestPage;

        [SetUp]
        public async Task Setup()
        {
            _playwrightSetup = new PlaywrightSetup();
            await _playwrightSetup.InitializeAsync();

            _context = await _playwrightSetup.CreateNewContextAsync();
            _page = await _context.NewPageAsync();
            _abTestPage = new ABTestPage(_page);

            await _abTestPage.NavigateAsync();
        }

        [Test]
        public async Task ABTesting01_ShouldHaveCorrectHeading()
        {
            var header = await _abTestPage.GetHeaderAsync();
            Assert.That(header, Does.Contain("A/B Test"), "Header is incorrect");
        }

        [TearDown]
        public async Task TearDown()
        {
            await _playwrightSetup.DisposeAsync();
        }
    }
}
