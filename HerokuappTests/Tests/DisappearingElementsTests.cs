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
    public class DisappearingElementsTests
    {
        PlaywrightSetup _playwrightSetup;
        IBrowserContext _context;
        IPage _page;
        DisappearingElementsPage _disappearingElementsPage;

        [SetUp]
        public async Task SetUp()
        {
            _playwrightSetup = new PlaywrightSetup();
            await _playwrightSetup.InitializeAsync();
            _context = await _playwrightSetup.CreateNewContextAsync();
            _page = await _context.NewPageAsync();

            _disappearingElementsPage = new DisappearingElementsPage(_page);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _playwrightSetup.DisposeAsync();
        }

        [Test]
        public async Task DisappearingElements01_ElementsAppearAtLeastOnce()
        {
            var seenElements = new HashSet<string>();
            for (int i = 0; i < 10; i++)
            {
                await _disappearingElementsPage.NavigateToPageAsync();
                var elements = await _disappearingElementsPage.GetNavigationElementsAsync();

                foreach (var element in elements)
                {
                    string text = await element.TextContentAsync();
                    seenElements.Add(text);
                }

                if (seenElements.Count == 5) break;
            }

            Assert.That(seenElements, Does.Contain("Home"));
            Assert.That(seenElements, Does.Contain("About"));
            Assert.That(seenElements, Does.Contain("Contact Us"));
            Assert.That(seenElements, Does.Contain("Portfolio"));
            Assert.That(seenElements, Does.Contain("Gallery"));
        }

        [Test]
        public async Task DisappearingElements02_CanChangeCount()
        {
            var elementCounts = new HashSet<int>();

            for (int i = 0; i < 10; i++)
            {
                await _disappearingElementsPage.NavigateToPageAsync();
                var count = await _disappearingElementsPage.GetNavigationElementsCountAsync();
                elementCounts.Add(count);

                if (elementCounts.Count > 1) break;
            }

            Assert.That(elementCounts.Count, Is.GreaterThan(1), "Liczba elementów nawigacyjnych nigdy się nie zmieniła.");
        }

        [Test]
        public async Task DisappearingElements03_ClickGalleryLink_WhenExists()
        {
            var expectedLinks = new Dictionary<string, string>
            {
                { "Home", "https://the-internet.herokuapp.com/" },
                { "About", "about" },
                { "Contact Us", "contact" },
                { "Portfolio", "portfolio" },
                { "Gallery", "gallery" }
            };

            foreach (var link in expectedLinks.Keys)
            {
                bool found = false;

                for (int i = 0; i < 10; i++)
                {
                    await _disappearingElementsPage.NavigateToPageAsync();

                    if (await _disappearingElementsPage.IsNavigationLinkPresentAsync(link))
                    {
                        await _disappearingElementsPage.ClickNavigationLinkAsync(link);

                        if (link == "Home")
                        {
                            Assert.That(await _disappearingElementsPage.GetCurrentUrlAsync(), Is.EqualTo(expectedLinks[link]),
                                $"Clicking on ‘{link}’ did not change the URL to the expected one.");
                        }
                        else
                        {
                            Assert.That(await _disappearingElementsPage.GetCurrentUrlAsync(), Does.Contain(expectedLinks[link]),
                                $"Clicking on ‘{link}’ did not change the URL to the expected one..");
                        }
                        found = true;
                        break;
                    }
                }

                Assert.That(found, Is.True, $"Element: {link} does not found after 10 refreshes.");
            }
        }
    }
}
