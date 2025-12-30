using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components
{
    public class ProductReview
    {
        // Instead of IPage, we use ILocator to scope the product review
        private readonly ILocator _container;

        private ILocator ReviewRatingStars => _container.Locator("review-rating-stars");

        public ILocator ReviewAuthor => _container.GetByTestId("review-userName");
        public ILocator ReviewText => _container.GetByTestId("review-text");
        public ILocator ReviewDate => _container.GetByTestId("review-date");

        public ProductReview(ILocator container)
        {
            _container = container;
        }

        public async Task<int> GetReviewRatingAsync()
        {
            var starsCount = await ReviewRatingStars.Locator("span.text-yellow-500").CountAsync();
            return (int)starsCount;
        }
    }
}