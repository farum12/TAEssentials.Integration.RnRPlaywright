using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Components.Interfaces
{
    public interface IDropdown
    {
        ILocator DropdownLocator { get; }
        ILocator PlaceholderLocator { get; }
        ILocator SelectedValue { get; }
        ILocator RemoveButton { get; }
        IDropdown SetDropdownLocator(string dropdownLocator);
        IDropdown SetDropdownLocator(ILocator dropdownLocator);
        Task TypeAndClickAsync(string optionName);
    }
}