using Reqnroll;
using TAEssentials.UI.Constants;
using TAEssentials.UI.DataClasses;
using TAEssentials.UI.PageObjects.Pages;

namespace TAEssentials.UI.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly MainPage _mainPage;
        private readonly LoginPage _loginPage;

        public LoginSteps(ScenarioContext scenarioContext, MainPage mainPage, LoginPage loginPage)
        {
            _scenarioContext = scenarioContext;
            _mainPage = mainPage;
            _loginPage = loginPage;
        }

        [Given(@"User navigates to the application")]
        public async Task GivenAPINewStandardUserIsExistsInTheSystem()
        {
            await _mainPage.GotoAsync();
        }

        [Given("User is logged in as Standard User")]
        public async Task GivenUserIsLoggedInAsStandardUser()
        {
            var user = _scenarioContext.Get<User>(ScenarioContextConstants.User);
            await _mainPage.HeaderBar.LoginNavButton.ClickAsync();
            await _loginPage.LoginAsync(user.Username, user.Password);
        }
    }
}