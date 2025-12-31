using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components
{
    /// <summary>
    /// Represents the wishlist item component on the wishlist page.
    /// Main Page -> Wishlist Page -> Wishlist Item Component
    /// </summary>
    public class WishlistItem
    {
        // Instead of IPage, we use ILocator to scope the product card
        private readonly ILocator _container;

        public ILocator Container => _container;
        public ILocator Title => _container.Locator("[data-testid^='wishlist-item-name-']");
        public ILocator Author => _container.Locator("[data-testid^='wishlist-item-author-']");
        public ILocator Price => _container.Locator("[data-testid^='wishlist-item-price-']");
        public ILocator AddToCartButton => _container.Locator("[data-testid^='wishlist-add-to-cart-']");
        public ILocator RemoveButton => _container.Locator("[data-testid^='wishlist-remove-']");
        public WishlistItem(ILocator container)
        {
            _container = container;
        }
    }
}