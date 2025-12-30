using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components;

public class FiltersBar
{
    private readonly IPage _page;

    public ILocator SearchInput => _page.GetByTestId("search-input");
    public ILocator GenreDropdown => _page.GetByTestId("genre-filter");
    public ILocator SortByDropdown => _page.GetByTestId("sort-by-filter");
    public ILocator OrderDropdown => _page.GetByTestId("sort-order-filter");
    public ILocator ClearFiltersButton => _page.GetByTestId("clear-filters-button");
    public ILocator SearchButton => _page.GetByTestId("search-button");

    public FiltersBar(IPage page)
    {
        _page = page;
    }
        
    public async Task SelectGenreAsync(string genre)
    {
        await GenreDropdown.SelectOptionAsync(genre);
    }

    public async Task SelectSortByAsync(string sortOption)
    {
        await SortByDropdown.SelectOptionAsync(sortOption);
    }

    public async Task SelectOrderAsync(string order)
    {
        await OrderDropdown.SelectOptionAsync(order);
    }

    public async Task ClearFiltersAsync()
    {
        await ClearFiltersButton.ClickAsync();
    }

}
