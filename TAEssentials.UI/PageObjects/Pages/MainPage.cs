using Microsoft.Playwright;
using TAEssentials.UI.PageObjects.Components;

namespace TAEssentials.UI.PageObjects.Pages
{
    public class MainPage
    {
        private readonly IPage _page;

        public HeaderBar HeaderBar { get; private set; }
        
        public MainPage(IPage page, HeaderBar headerBar)
        {
            _page = page;
            HeaderBar = headerBar;
        }
        
        public async Task GotoAsync()
        {
            //TODO: Move URL to configuration
            await _page.GotoAsync("http://localhost:3000/");
        }
    }
}