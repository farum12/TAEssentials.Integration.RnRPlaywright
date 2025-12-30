using Microsoft.Playwright;

namespace TAEssentials.UI.PageObjects.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public ILocator UsernameInput => _page.GetByTestId("login-username-input");
    public ILocator PasswordInput => _page.GetByTestId("login-password-input");
    public ILocator SignInButton => _page.GetByTestId("login-submit-button");

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await SignInButton.ClickAsync();
    }
}
