using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components
{
    public class ProductCard
    {
        // Instead of IPage, we use ILocator to scope the product card
        private readonly ILocator _container;

        public ILocator ProductName => _container.Locator("[data-testid^='product-name-']");
        public ILocator ProductAuthor => _container.Locator("[data-testid^='product-author-']");
        public ILocator ProductPrice => _container.Locator("[data-testid^='product-price-']");
        public ILocator ProductStockStatus => _container.Locator("[data-testid^='product-stock-status-']");
        public ILocator ProductViewDetailsButton => _container.Locator("[data-testid^='product-view-button']");

        public ProductCard(ILocator container)
        {
            _container = container;
        }
    }
}