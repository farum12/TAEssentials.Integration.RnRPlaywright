using Reqnroll;
using TAEssentials.UI.PageObjects;

namespace TAEssentials.UI.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly MainPage _mainPage;

        public LoginSteps(ScenarioContext scenarioContext, MainPage mainPage)
        {
            _scenarioContext = scenarioContext;
            _mainPage = mainPage;
        }

        
        [Given(@"User navigates to the application")]
        [When("User navigates to Main Page")]
        public async Task GivenAPINewStandardUserIsExistsInTheSystem()
        {
            await _mainPage.GotoAsync();
        }

    // This step definition uses Cucumber Expressions. See https://github.com/gasparnagy/CucumberExpressions.SpecFlow
    [Given("User is logged in as Standard User")]
    public void GivenUserIsLoggedInAsStandardUser()
    {
        // Write code here that turns the phrase above into concrete actions
    }
    }
}