using System.ComponentModel;
using Microsoft.Playwright;

namespace TAEssentials.UI.Extensions
{
    public static class PageExtensions
    {
        public static async Task WaitUntilIsLoadedAsync(this IPage page, ILocator? containerLocator = null)
        {
            const string spinnerSelector = ".spinner, .loading, .loader, .progress-indicator";

            var spinnerLocator = containerLocator == null 
                ? page.Locator(spinnerSelector)
                : containerLocator.Locator(spinnerSelector);
            
            await spinnerLocator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Detached
            });
        }

        
    }
}