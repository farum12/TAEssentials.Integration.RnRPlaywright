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
            var reviewContainer = _page
                .GetByTestId("review-userName")
                .Filter(new LocatorFilterOptions { HasText = authorName })
                .First;

            return _productReviewFactory(reviewContainer);
        }
    }
}