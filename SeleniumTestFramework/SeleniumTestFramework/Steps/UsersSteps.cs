using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class UsersSteps
    {
        private readonly IWebDriver _driver;
        private readonly SettingsModel _settingsModel;
        private readonly ScenarioContext _scenarioContext;
        private UsersPage _usersPage;

        public UsersSteps(IWebDriver driver, SettingsModel settingsModel, ScenarioContext scenarioContext, UsersPage usersPage)
        {
            _driver = driver;
            _settingsModel = settingsModel;
            _scenarioContext = scenarioContext;
            _usersPage = usersPage;
        }

        [When("I delete the created user")]
        public void WhenIDeleteTheCreatedUser()
        {
            var user = (RegisterModel)_scenarioContext["RegisteredUser"];
            _usersPage.DeleteUser(user.Email);
        }

        [Then("the new user should be present in the users list")]
        public void ThenTheNewUserShouldBePresentInTheUsersList()
        {
            _usersPage.VerifyIsAtUsersPage(true);
            var newUser = (RegisterModel)_scenarioContext["RegisteredUser"];
            _usersPage.VerifyUserExists(newUser.Email);
        }

        [Then("the user should no longer be present in the users list")]
        public void ThenTheUserShouldNoLongerBePresentInTheUsersList()
        {
            var newUser = (RegisterModel)_scenarioContext["RegisteredUser"];
            _usersPage.VerifyUserDoesNotExist(newUser.Email);
        }
    }
}
