using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Pages
{
    public class ProductPage
    {
        private readonly IPage _page;

        public ProductPage(IPage page)
        {
            _page = page;
        }
    }
}