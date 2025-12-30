using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components
{
    public class ProductInfo
    {
        private readonly IPage _page;

        public ILocator ProductName => _page.GetByTestId("product-title");
        public ILocator ProductAuthor => _page.GetByTestId("product-author");
        public ILocator ProductPrice => _page.GetByTestId("product-price");
        public ILocator ProductGenre => _page.GetByTestId("product-genre");
        public ILocator ProductStockStatus => _page.GetByTestId("product-stock-status");
        public ILocator ProductViewDetailsButton => _page.GetByTestId("product-view-button");

        public ProductInfo(IPage page)
        {
            _page = page;
        }
    }
}