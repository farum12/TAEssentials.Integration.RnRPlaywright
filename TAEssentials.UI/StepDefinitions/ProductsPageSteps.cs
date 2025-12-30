using Reqnroll;
using TAEssentials.UI.Constants;
using TAEssentials.UI.DataClasses;
using TAEssentials.UI.PageObjects.Pages;

namespace TAEssentials.UI.StepDefinitions
{
    [Binding]
    public class ProductsPageSteps
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly ProductsPage _productsPage;

        public ProductsPageSteps(ScenarioContext scenarioContext, ProductsPage productsPage)
        {
            _scenarioContext = scenarioContext;
            _productsPage = productsPage;
        }

        [When("User searches New Book by its title")]
        public async Task WhenUserSearchesNewBookByItsTitle()
        {
            var book = _scenarioContext.Get<Book>(ScenarioContextConstants.Book);
            await _productsPage.FiltersBar.SearchInput.FillAsync(book.Name);
            await _productsPage.FiltersBar.SearchButton.ClickAsync();
            await _productsPage.ProductsGrid.WaitUntilIsLoadedAsync();
        }

        [When("User clicks on New Book View Details button")]
        public async Task WhenUserClicksOnNewBookViewDetailsButton()
        {
            var book = _scenarioContext.Get<Book>(ScenarioContextConstants.Book);
            var targetProductCard = _productsPage.ProductsGrid.GetProductCardByName(book.Name);
            await targetProductCard.ProductViewDetailsButton.ClickAsync();
        }
    }
}