using NUnit.Framework;
using PlaywrightTests.Pages;
using Microsoft.Playwright;
using System.Threading.Tasks;
using HerokuappTests.Helpers;

namespace PlaywrightTests
{
    [TestFixture]
    [Category("Herokuapp")]
    public class BrokenImagesTests
    {
        private PlaywrightSetup _playwrightSetup;
        private IPage _page;
        private IBrowserContext _context;
        private BrokenImagesPage _brokenImagesPage;

        [SetUp]
        public async Task SetUp()
        {
            _playwrightSetup = new PlaywrightSetup();
            await _playwrightSetup.InitializeAsync();

            _context = await _playwrightSetup.CreateNewContextAsync();
            _page = await _context.NewPageAsync();
            _brokenImagesPage = new BrokenImagesPage(_page);
        }

        [TearDown]
        public async Task TearDown()
        {
            await _playwrightSetup.DisposeAsync();
        }

        [Test]
        public async Task BrokenImages01_AllImagesAreVisible()
        {
            await _brokenImagesPage.NavigateToBrokenImagesAsync();

            var imageCount = await _brokenImagesPage.ImageElements.CountAsync();

            for (int i = 0; i < imageCount; i++)
            {
                bool isImageLoaded = await _brokenImagesPage.IsImageVisibleAsync(i);
                Assert.IsTrue(isImageLoaded, $"Image {i} did not load correctly.");
            }
        }

        [Test]
        public async Task BrokenImages02_CorrectSrc()
        {
            await _brokenImagesPage.NavigateToBrokenImagesAsync();
            var imageCount = await _brokenImagesPage.ImageElements.CountAsync();

            for (int i = 0; i < imageCount; i++)
            {
                var imageSrc = await _brokenImagesPage.GetImageSrcAsync(i);
                bool isImageVisible = await _brokenImagesPage.IsImageVisibleAsync(i);

                if (isImageVisible)
                {
                    Assert.IsTrue(imageSrc.Contains("img"), $"Image {i} has incorrect src: {imageSrc}.");
                }
                else
                {
                    Assert.Fail($"Imgae {i} is not visible.");
                }
            }
        }
    }
}
