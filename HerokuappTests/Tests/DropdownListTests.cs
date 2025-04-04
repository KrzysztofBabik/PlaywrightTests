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
    public class DropdownListTests
    {
        PlaywrightSetup _playwrightSetup;
        IBrowserContext _context;
        IPage _page;
        DropdownListPage _dropdownListPage;
        
        [SetUp]
        public async Task SetUp()
        {
            _playwrightSetup = new PlaywrightSetup();
            await _playwrightSetup.InitializeAsync();
            _context = await _playwrightSetup.CreateNewContextAsync();
            _page = await _context.NewPageAsync();

            _dropdownListPage = new DropdownListPage(_page);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _playwrightSetup.DisposeAsync();
        }

        [Test]
        public async Task DropdownList01_DropdownExistsOnPage()
        {
            await _dropdownListPage.NavigateToPageAsync();

            Assert.That(await _dropdownListPage.Dropdown.IsVisibleAsync(), Is.True,
                "Dropdown menu not visible on page.");
        }

        [Test]
        public async Task DropdownList02_DropdownDefaultSelection()
        {
            await _dropdownListPage.NavigateToPageAsync();

            var selectedValue = await _dropdownListPage.GetSelectedOptionValueAsync();
            Assert.That(selectedValue, Is.EqualTo(""),
                "By default, no option should be selected.");
        }

        [Test]
        public async Task DropdownList03_DropdownContainExpectedOptions()
        {
            await _dropdownListPage.NavigateToPageAsync();

            var options = await _dropdownListPage.GetAllDropdownOptionsAsync();
            var expectedOptions = new List<string> { "Please select an option", "Option 1", "Option 2" };

            CollectionAssert.AreEquivalent(expectedOptions, options,
                "Dropdown does not include expected options.");
        }

        [TestCase("Option 1")]
        [TestCase("Option 2")]
        public async Task DropdownList04_DropdownCanSelectOption(string optionToSelect)
        {
            await _dropdownListPage.NavigateToPageAsync();

            await _dropdownListPage.SelectDropdownOptionAsync(optionToSelect);
            var selectedText = await _dropdownListPage.GetSelectedOptionTextAsync();

            Assert.That(selectedText, Is.EqualTo(optionToSelect),
                $"Failed to select option: {optionToSelect}");
        }

        [Test]
        public async Task DropdownList05_AllowChangingSelection()
        {
            await _dropdownListPage.NavigateToPageAsync();

            await _dropdownListPage.SelectDropdownOptionAsync("Option 1");
            var firstSelection = await _dropdownListPage.GetSelectedOptionTextAsync();

            await _dropdownListPage.SelectDropdownOptionAsync("Option 2");
            var secondSelection = await _dropdownListPage.GetSelectedOptionTextAsync();

            Assert.That(firstSelection, Is.Not.EqualTo(secondSelection),
                "Dropdown does not change value after reselection.");
        }
    }
}
