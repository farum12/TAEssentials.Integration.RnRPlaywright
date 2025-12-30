using System.ComponentModel;
using Microsoft.Playwright;

namespace TAEssentials.UI.Extensions
{
    public static class PageExtensions
    {
        public static async Task WaitUntilIsLoadedAsync(this IPage page, ILocator? containerLocator = null)
        {
            const string spinnerSelector = "spinner-icon";

            var spinnerLocator = containerLocator == null 
                ? page.GetByTestId(spinnerSelector)
                : containerLocator.GetByTestId(spinnerSelector);
            
            await spinnerLocator.WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Detached
            });
        }

        
    }
}