using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class DashboardSteps
    {
        private readonly IWebDriver _driver;
        private readonly SettingsModel _settingsModel;
        private readonly ScenarioContext _scenarioContext;
        private DashboardPage _dashboardPage;

        public DashboardSteps(IWebDriver webDriver, SettingsModel model, ScenarioContext scenarioContext, DashboardPage dashboardPage)
        {
            this._driver = webDriver;
            this._settingsModel = model;
            this._scenarioContext = scenarioContext;
            this._dashboardPage = dashboardPage;
        }

        [Given("the user can see the dashboard with its data")]
        public void GivenTheUserCanSeeTheDashboardWithItsData()
        {
            var newUser = (RegisterModel)_scenarioContext["RegisteredUser"];
            _dashboardPage.VerifyIsAtDashboardPage();
            _dashboardPage.VerifyUserIsLoggedIn(newUser.Email, $"{newUser.FirstName} {newUser.Surname}", false);
        }

        [Given("the user should be able to logout successfully")]
        public void GivenTheUserShouldBeAbleToLogoutSuccessfully()
        {
            _dashboardPage.Logout();
        }

        [When("navigates to the users page")]
        public void WhenNavigatesToTheUsersPage()
        {
            _dashboardPage.GoToUsersPage();
        }

        [When("the administrator logs out successfully")]
        public void WhenTheAdministratorLogsOutSuccessfully()
        {
            _dashboardPage.Logout();
        }


        [Then("I should see the logged user in the main header")]
        public void ThenIShouldSeeTheLoggedUserInTheMainHeader()
        {
            _dashboardPage.VerifyLoggedUserEmailIs(_settingsModel.Email);
            _dashboardPage.VerifyUsernameIs(_settingsModel.Username);
        }

        [Then("I should see the logged user {string} in the navbar dropdown")]
        public void ThenIShouldSeeTheLoggedUserInTheNavbarDropdown(string expectedEmail)
        {
            if (expectedEmail == "readFromSettings")
            {
                expectedEmail = _settingsModel.Email;
            }

            _dashboardPage.VerifyLoggedUserEmailIs(expectedEmail);
        }

        [Then("I should be able to logout successfully")]
        public void ThenIShouldBeAbleToLogoutSuccessfully()
        {
            _dashboardPage.Logout();
        }
    }
}
