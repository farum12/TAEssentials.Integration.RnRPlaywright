using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components
{
    public class HeaderBar
    {
        private readonly IPage _page;

        public ILocator HeaderLogo => _page.GetByTestId("header-logo");
        public ILocator ProductsNavButton => _page.GetByTestId("nav-products");
        public ILocator CartNavButton => _page.GetByTestId("nav-cart");
        public ILocator WishlistNavButton => _page.GetByTestId("nav-wishlist");
        public ILocator LoginNavButton => _page.GetByTestId("nav-login");
        public ILocator RegisterNavButton => _page.GetByTestId("nav-register");

        public HeaderBar(IPage page)
        {
            _page = page;
        }
    }
}