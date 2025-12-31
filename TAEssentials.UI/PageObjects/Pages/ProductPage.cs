using Microsoft.Playwright;
using TAEssentials.UI.PageObjects.Components;

namespace TAEssentials.UI.PageObjects.Pages
{
    public class ProductPage
    {
        private readonly IPage _page;

        public ILocator WishlistButton => _page.GetByTestId("wishlist-button");

        public ProductInfo ProductInfo { get; private set; }
        public ProductReviewsList ReviewsGrid { get; private set; }
        public ProductWriteReview WriteReview { get; private set; }

        public ProductPage(IPage page, ProductInfo productInfo, ProductReviewsList productReviewsGrid, ProductWriteReview productWriteReview)
        {
            _page = page;
            ProductInfo = productInfo;
            ReviewsGrid = productReviewsGrid;
            WriteReview = productWriteReview;
        }
    }
}