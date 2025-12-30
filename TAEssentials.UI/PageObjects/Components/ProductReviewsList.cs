using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components
{
    /// <summary>
    /// Represents the product reviews grid component on the product page.
    /// </summary>
    public class ProductReviewsList
    {
        private readonly IPage _page;
        private readonly Func<ILocator, ProductReview> _productReviewFactory;

        private ILocator Container => _page.GetByTestId("reviews-section");

        public ILocator WriteReviewButton => _page.GetByTestId("write-review-button");

        public ProductReviewsList(IPage page, Func<ILocator, ProductReview> productReviewFactory)
        {
            _page = page;
            _productReviewFactory = productReviewFactory;
        }

        public ProductReview GetProductReviewByAuthor(string authorName)
        {
            // Znajdujemy kontener review-card, który zawiera userName z danym tekstem
            var reviewContainer = _page
                .Locator("[data-testid^='review-card-']")
                .Filter(new LocatorFilterOptions 
                { 
                    Has = _page.GetByTestId("review-userName").Filter(new LocatorFilterOptions { HasText = authorName })
                })
                .First;

            return _productReviewFactory(reviewContainer);
        }
    }
}