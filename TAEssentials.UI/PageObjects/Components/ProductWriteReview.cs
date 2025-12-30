using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components
{
    /// <summary>
    /// Represents the product review write component on the product page.
    /// Main Page -> Product Page -> Product Reviews Grid -> 'Write Review' Button -> Product Write Review Component
    /// </summary>
    public class ProductWriteReview
    {
        private readonly IPage _page;

        public ILocator RatingStar1 => _page.GetByTestId("rating-star-1");
        public ILocator RatingStar2 => _page.GetByTestId("rating-star-2");
        public ILocator RatingStar3 => _page.GetByTestId("rating-star-3");
        public ILocator RatingStar4 => _page.GetByTestId("rating-star-4");
        public ILocator RatingStar5 => _page.GetByTestId("rating-star-5");
        public ILocator ReviewTextInput => _page.GetByTestId("review-text-input");
        public ILocator SubmitReviewButton => _page.GetByTestId("submit-review-button");

        public ProductWriteReview(IPage page)
        {
            _page = page;
        }
    }
}