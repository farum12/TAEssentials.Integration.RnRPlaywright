using Microsoft.Playwright;
using TAEssentials.UI.Extensions;

namespace TAEssentials.UI.PageObjects.Components
{
    public class ProductsGrid
    {
        private readonly IPage _page;
        private readonly Func<ILocator, ProductCard> _productCardFactory;

        private ILocator Container => _page.GetByTestId("products-grid");

        public ProductsGrid(IPage page, Func<ILocator, ProductCard> productCardFactory)
        {
            _page = page;
            _productCardFactory = productCardFactory;
        }

        public ProductCard GetProductCardByName(string productName)
        {
            var cardContainer = _page
                .Locator("[data-testid^='product-card-']")
                .Filter(new LocatorFilterOptions { HasText = productName })
                .First;

            return _productCardFactory(cardContainer);
        }

        public async Task WaitUntilIsLoadedAsync()
        {
            await _page.WaitUntilIsLoadedAsync(Container);
        }
    }
}