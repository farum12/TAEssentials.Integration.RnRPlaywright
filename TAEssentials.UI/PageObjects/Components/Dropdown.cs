using Microsoft.Playwright;
using TAEssentials.UI.PageObjects.Components.Interfaces;

namespace TAEssentials.UI.PageObjects.Components;

// An example how to deal with Reqnroll's InstancePerDependency, as presented on  Dropdown component
public class Dropdown: IDropdown
{
    private readonly IPage _page;

    public Dropdown(IPage page)
    {
        _page = page;
    }

    public ILocator DropdownLocator {get; private set; }

    public ILocator PlaceholderLocator => DropdownLocator.GetByTestId("dropdown-placeholder");

    public ILocator SelectedValue => DropdownLocator.GetByTestId("dropdown-selected-value");

    public ILocator RemoveButton => DropdownLocator.GetByTestId("dropdown-remove-button");

    public IDropdown SetDropdownLocator(string dropdownLocator)
    {
        DropdownLocator = _page.Locator(dropdownLocator);
        return this;
    }

    public IDropdown SetDropdownLocator(ILocator dropdownLocator)
    {
        DropdownLocator = dropdownLocator;
        return this;
    }

    public async Task TypeAndClickAsync(string optionName)
    {
        await DropdownLocator.ScrollIntoViewIfNeededAsync();
        await DropdownLocator.ClickAsync();
        await DropdownLocator.PressSequentiallyAsync(optionName);
        await DropdownLocator.Locator($"text={optionName}").ClickAsync();
    }
}