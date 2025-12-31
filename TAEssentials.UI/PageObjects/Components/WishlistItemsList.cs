using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components
{
    /// <summary>
    /// Represents the product reviews grid component on the product page.
    /// </summary>
    public class WishlistItemsList
    {
        private readonly IPage _page;
        private readonly Func<ILocator, WishlistItem> _wishlistItemFactory;

        public WishlistItemsList(IPage page, Func<ILocator, WishlistItem> wishlistItemFactory)
        {
            _page = page;
            _wishlistItemFactory = wishlistItemFactory;
        }

        public WishlistItem GetWishlistItemByAuthor(string bookTitle)
        {
            var reviewContainer = _page
                .Locator("[data-testid^='wishlist-item-container-']")
                .Filter(new LocatorFilterOptions 
                { 
                    Has = _page.Locator("[data-testid^='wishlist-item-name-']").Filter(new LocatorFilterOptions { HasText = bookTitle })
                })
                .First;

            return _wishlistItemFactory(reviewContainer);
        }
    }
}