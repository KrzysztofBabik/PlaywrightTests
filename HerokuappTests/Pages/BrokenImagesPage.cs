using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlaywrightTests.Pages
{
    public class BrokenImagesPage
    {
        private readonly IPage _page;

        public BrokenImagesPage(IPage page)
        {
            _page = page;
        }

        public ILocator ImageElements => _page.Locator("img");

        public async Task NavigateToBrokenImagesAsync()
        {
            await _page.GotoAsync("https://the-internet.herokuapp.com/broken_images");
        }

        public async Task<bool> IsImageVisibleAsync(int index)
        {
            var image = ImageElements.Nth(index);
            return await image.IsVisibleAsync();
        }

        // Funkcja, która sprawdza, czy dany obrazek jest broken
        public async Task<bool> IsImageBrokenAsync(int index)
        {
            var image = ImageElements.Nth(index);
            return !await image.IsVisibleAsync();
        }

        public async Task<string> GetImageSrcAsync(int index)
        {
            var image = ImageElements.Nth(index);
            return await image.GetAttributeAsync("src");
        }
    }
}
