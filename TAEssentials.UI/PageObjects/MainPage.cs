using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects
{
    public class MainPage
    {
        private readonly IPage _page;
        
        public async Task GotoAsync()
        {
            //TODO: Move URL to configuration
            await _page.GotoAsync("http://localhost:3000/");
        }
    }
}