using Reqnroll;
using TAEssentials.UI.PageObjects.Pages;

namespace TAEssentials.UI.StepDefinitions
{
    [Binding]
    public class NavigationSteps
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly MainPage _mainPage;

        public NavigationSteps(ScenarioContext scenarioContext, MainPage mainPage)
        {
            _scenarioContext = scenarioContext;
            _mainPage = mainPage;
        }
        
        [When("User clicks Header Logo in order to navigate to Main Page")]
        public async Task WhenUserClicksHeaderLogoInOrderToNavigateToMainPage()
        {
            await _mainPage.HeaderBar.HeaderLogo.ClickAsync();
        }

        [When("User clicks Products Nav Button in order to navigate to Products Page")]
        public async Task WhenUserClicksProductsNavButtonInOrderToNavigateToProductsPage()
        {
            await _mainPage.HeaderBar.ProductsNavButton.ClickAsync();
        }

        [When("User clicks on Wishlist Nav Button in order to navigate to Wishlist Page")]
        public async Task WhenUserClicksOnWishlistNavButtonInOrderToNavigateToWishlistPage()
        {
            await _mainPage.HeaderBar.WishlistNavButton.ClickAsync();
        }

        [When("User clicks on Cart Nav Button in order to navigate to Cart Page")]
        public async Task WhenUserClicksOnCartNavButtonInOrderToNavigateToCartPage()
        {
            await _mainPage.HeaderBar.CartNavButton.ClickAsync();
        }

    }
}