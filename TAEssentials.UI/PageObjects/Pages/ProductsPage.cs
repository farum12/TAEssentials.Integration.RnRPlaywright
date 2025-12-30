using Microsoft.Playwright;
using TAEssentials.UI.PageObjects.Components;

namespace TAEssentials.UI.PageObjects.Pages
{
    public class ProductsPage
    {
        private readonly IPage _page;

        public FiltersBar FiltersBar { get; private set; }
        public ProductsGrid ProductsGrid { get; private set; }

        public ProductsPage(IPage page, FiltersBar filtersBar, ProductsGrid productsGrid)
        {
            _page = page;
            FiltersBar = filtersBar;
            ProductsGrid = productsGrid;
        }
    }
}