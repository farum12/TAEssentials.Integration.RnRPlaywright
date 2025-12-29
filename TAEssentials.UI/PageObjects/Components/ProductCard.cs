using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components
{
    public class ProductCard
    {
        private readonly IPage _page;

        public ILocator ProductName => _page.Locator("product-name-XXX");
        public ILocator ProductAuthor => _page.Locator("product-author-XXX");
        public ILocator ProductPrice => _page.Locator("product-price-XXX");
        public ILocator ProductStockStatus => _page.Locator("product-stock-status-XXX");
        public ILocator ProductViewDetailsButton => _page.Locator("product-view-button-button-XXX");

        public ProductCard(IPage page)
        {
            _page = page;
        }
    }
}