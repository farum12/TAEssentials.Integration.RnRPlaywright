using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects
{
    public class ProductsPage
    {
        private readonly IPage _page;

        public ProductsPage(IPage page)
        {
            _page = page;
        }
    }
}