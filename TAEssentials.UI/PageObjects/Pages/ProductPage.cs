using Microsoft.Playwright;
using TAEssentials.UI.PageObjects.Components;

namespace TAEssentials.UI.PageObjects.Pages
{
    public class ProductPage
    {
        private readonly IPage _page;

        public ProductInfo ProductInfo { get; private set; }
        public ProductReviewsGrid ReviewsGrid { get; private set; }
        public ProductWriteReview WriteReview { get; private set; }

        public ProductPage(IPage page, ProductInfo productInfo, ProductReviewsGrid productReviewsGrid, ProductWriteReview productWriteReview)
        {
            _page = page;
            ProductInfo = productInfo;
            ReviewsGrid = productReviewsGrid;
            WriteReview = productWriteReview;
        }
    }
}