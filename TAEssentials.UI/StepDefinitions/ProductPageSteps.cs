using FluentAssertions;
using Microsoft.Playwright;
using Reqnroll;
using TAEssentials.UI.Constants;
using TAEssentials.UI.DataClasses;
using TAEssentials.UI.PageObjects.Pages;

namespace TAEssentials.UI.StepDefinitions
{
    [Binding]
    public class ProductPageSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ProductPage _productPage;

        private BookReview _bookReview;


        public ProductPageSteps(ScenarioContext scenarioContext, ProductPage productPage, DataGenerator dataGenerator)
        {
            _scenarioContext = scenarioContext;
            _productPage = productPage;
            _bookReview = dataGenerator.FakeBookReview;
        }

        [When("User adds a book review with rating {string} and some comment")]
        public async Task WhenUserAddsABookReviewWithRatingAndSomeComment(string rating)
        {
            _bookReview.Rating = int.Parse(rating);
            switch (_bookReview.Rating)
            {
                case 1:
                    await _productPage.WriteReview.RatingStar1.ClickAsync();
                    break;
                case 2:
                    await _productPage.WriteReview.RatingStar2.ClickAsync();
                    break;
                case 3:
                    await _productPage.WriteReview.RatingStar3.ClickAsync();
                    break;
                case 4:
                    await _productPage.WriteReview.RatingStar4.ClickAsync();
                    break;
                case 5:
                    await _productPage.WriteReview.RatingStar5.ClickAsync();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid rating value");
            }

            await _productPage.WriteReview.ReviewTextInput.FillAsync(_bookReview.ReviewText);
            await _productPage.WriteReview.SubmitReviewButton.ClickAsync();
        }

        [When("User clicks on Write a Review button")]
        public async Task WhenUserClicksOnWriteAReviewButton()
        {
            await _productPage.ReviewsGrid.WriteReviewButton.ClickAsync();
        }

        [Then("The book review should be successfully submitted")]
        public async Task ThenTheBookReviewShouldBeSuccessfullySubmitted()
        {
            var user = _scenarioContext.Get<User>(ScenarioContextConstants.User);
            var reviewByUser = _productPage.ReviewsGrid.GetProductReviewByAuthor(user.Username);
            await Assertions.Expect(reviewByUser.ReviewText).ToBeVisibleAsync();
            await Assertions.Expect(reviewByUser.ReviewText).ToHaveTextAsync(_bookReview.ReviewText);
            
            var actualRating =  await reviewByUser.GetReviewRatingAsync();
            actualRating.Should().Be(_bookReview.Rating);

        }

        [When("User clicks on Add to Wishlist button of the Book Details Page")]
        public async Task WhenUserClicksOnAddToWishlistButtonOfTheBookDetailsPage()
        {
            await _productPage.WishlistButton.ClickAsync();
        }
    }
}