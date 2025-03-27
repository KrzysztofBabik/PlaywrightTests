using HerokuappTests.Helpers;
using HerokuappTests.Pages;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerokuappTests.Tests
{
    [TestFixture]
    [Category("Herokuapp")]
    public class DragAndDropTests
    {
        PlaywrightSetup _playwrightSetup;
        DragAndDropPage _dragAndDropPage;
        IBrowserContext _context;
        IPage _page;

        [SetUp]
        public async Task SetUp()
        {
            _playwrightSetup = new PlaywrightSetup();
            await _playwrightSetup.InitializeAsync();
            _context = await _playwrightSetup.CreateNewContextAsync();
            _page = await _context.NewPageAsync();

            _dragAndDropPage = new DragAndDropPage(_page);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _playwrightSetup.DisposeAsync();
        }

        [Test]
        public async Task DragAndDrop01_MoveColumnAToColumnB()
        {
            await _dragAndDropPage.NavigateToPageAsyc();

            string initTextColA = await _dragAndDropPage.GetTextFromColumnA();
            string initTextColB = await _dragAndDropPage.GetTextFromColumnB();

            Assert.That(initTextColA, Is.Not.EqualTo(initTextColB), "Texts are the same. Cannot check if drag and drop works");

            await _dragAndDropPage.DragAAndDropOnB();
            string newTextColB = await _dragAndDropPage.GetTextFromColumnB();

            Assert.That(newTextColB, Is.EqualTo(initTextColA), "Texts are not the same. Drag and drop does not work");
        }
    }
}
