using Microsoft.Playwright;
using TAEssentials.UI.Extensions;
using TAEssentials.UI.PageObjects.Components;

namespace TAEssentials.UI.PageObjects.Pages
{
    public class WishlistPage
    {
        private readonly IPage _page;

        public ILocator WishlistItemsContainer => _page.GetByTestId("wishlist-items-container");

        public ILocator BrowseProductsButton => _page.GetByTestId("browse-products-button");
        public ILocator MoveAllToCartButton => _page.GetByTestId("move-all-to-cart-button");
        public ILocator ClearWishlistButton => _page.GetByTestId("clear-wishlist-button");

        public WishlistItemsList ItemsList { get; private set; }

        public WishlistPage(IPage page, WishlistItemsList itemsList)
        {
            _page = page;
            ItemsList = itemsList;
        }

        public async Task ReloadAsync()
        {
            await _page.ReloadAsync();
            await _page.WaitUntilIsLoadedAsync(WishlistItemsContainer);
        }
    }
}