using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components
{
    /// <summary>
    /// Represents the product reviews grid component on the product page.
    /// </summary>
    public class ProductReviewsGrid
    {
        private readonly IPage _page;

        public ILocator WriteReviewButton => _page.GetByTestId("write-review-button");

        public ProductReviewsGrid(IPage page)
        {
            _page = page;
        }
    }
}