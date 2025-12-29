using System.Net;
using Reqnroll;
using TAEssentials.UI.DataClasses;
using TAEssentials.BE.Clients;
using TAEssentials.UI.Constants;
using TAEssentials.UI.Extensions;

namespace TAEssentials.UI.StepDefinitions.DataPreparation
{
    [Binding]
    public class UserSteps
    {
        private ScenarioContext _scenarioContext;
        private readonly AuthClient _authClient;
        private readonly DataGenerator _dataGenerator;

        public UserSteps(ScenarioContext scenarioContext, DataGenerator dataGenerator)
        {
            _scenarioContext = scenarioContext;
            _dataGenerator = dataGenerator;
        }

        
        [Given(@"API: New Standard User is exists in the system")]
        public async Task GivenAPINewStandardUserIsExistsInTheSystem()
        {
            var user = _dataGenerator.FakeUser;

            var userResponse = await _authClient.RegisterAsync(new()
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            });

            userResponse.Should().HaveHttpStatusCode(HttpStatusCode.Created);

            _scenarioContext.Add(ScenarioContextConstants.User, user);
        }

    }
}