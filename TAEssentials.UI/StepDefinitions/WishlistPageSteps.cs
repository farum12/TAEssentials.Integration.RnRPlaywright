using FluentAssertions;
using Microsoft.Playwright;
using Reqnroll;
using TAEssentials.UI.Constants;
using TAEssentials.UI.DataClasses;
using TAEssentials.UI.PageObjects.Pages;

namespace TAEssentials.UI.StepDefinitions
{
    [Binding]
    public class WishlistPageSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly WishlistPage _wishlistPage;

        public WishlistPageSteps(ScenarioContext scenarioContext, WishlistPage wishlistPage)
        {
            _scenarioContext = scenarioContext;
            _wishlistPage = wishlistPage;
        }

        [Then("the New Book should be displayed in the Wishlist Page")]
        public async Task ThenTheNewBookShouldBeDisplayedInTheWishlistPage()
        {
            var book = _scenarioContext.Get<Book>(ScenarioContextConstants.Book);
            var wishlistItem = _wishlistPage.ItemsList.GetWishlistItemByAuthor(book.Name);

            await Assertions.Expect(wishlistItem.Container).ToBeVisibleAsync();

            await Assertions.Expect(wishlistItem.Author).ToHaveTextAsync($"by {book.Author}");
            await Assertions.Expect(wishlistItem.Title).ToHaveTextAsync(book.Name);            
        }

        [Then("the New Book should not be displayed in the Wishlist Page anymore")]
        public async Task ThenTheNewBookShouldNotBeDisplayedInTheWishlistPageAnymore()
        {
            var book = _scenarioContext.Get<Book>(ScenarioContextConstants.Book);
            var wishlistItem = _wishlistPage.ItemsList.GetWishlistItemByAuthor(book.Name);

            await Assertions.Expect(wishlistItem.Container).ToBeHiddenAsync();
        }

        [When("User removes the New Book from the Wishlist Page")]
        public async Task WhenUserRemovesTheNewBookFromTheWishlistPage()
        {
            var book = _scenarioContext.Get<Book>(ScenarioContextConstants.Book);
            var wishlistItem = _wishlistPage.ItemsList.GetWishlistItemByAuthor(book.Name);

            await wishlistItem.RemoveButton.ClickAsync();
        }

        [When("User refreshes the Wishlist Page")]
        public async Task WhenUserRefreshesTheWishlistPage()
        {
            await _wishlistPage.ReloadAsync(); 

        }

        [When("User clicks on Move All to Cart button in the Wishlist Page")]
        public async Task WhenUserClicksOnMoveAllToCartButtonInTheWishlistPage()
        {
            await _wishlistPage.MoveAllToCartButton.ClickAsync();
        }
    }
}