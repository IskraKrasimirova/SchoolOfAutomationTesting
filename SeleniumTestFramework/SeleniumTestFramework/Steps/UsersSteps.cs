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
        private LoginPage _loginPage;
        private DashboardPage _dashboardPage;

        public UsersSteps(IWebDriver driver, SettingsModel settingsModel, ScenarioContext scenarioContext, UsersPage usersPage, LoginPage loginPage, DashboardPage dashboardPage)
        {
            _driver = driver;
            _settingsModel = settingsModel;
            _scenarioContext = scenarioContext;
            _usersPage = usersPage;
            _loginPage = loginPage;
            _dashboardPage = dashboardPage;
        }

        [When("I delete the created user")]
        public void WhenIDeleteTheCreatedUser()
        {
            var user = (RegisterModel)_scenarioContext["RegisteredUser"];
            _usersPage.DeleteUser(user.Email);
        }

        [When("I login with admin credentials, navigate to the users page, delete the created user, and log out")]
        public void WhenILoginWithAdminCredentialsNavigateToTheUsersPageDeleteTheCreatedUserAndLogout()
        {
            _loginPage.VerifyIsAtLoginPage();
            _loginPage.LoginWith(_settingsModel.Email, _settingsModel.Password);

            _dashboardPage.VerifyIsAtDashboardPage();
            _dashboardPage.VerifyUserIsLoggedIn(_settingsModel.Email, _settingsModel.Username, true);
            _dashboardPage.GoToUsersPage();

            _usersPage.VerifyIsAtUsersPage(true);
            var user = (RegisterModel)_scenarioContext["RegisteredUser"];
            _usersPage.VerifyUserExists(user.Email);
            _usersPage.DeleteUser(user.Email);
            _usersPage.VerifyUserDoesNotExist(user.Email);

            _dashboardPage.Logout();
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
