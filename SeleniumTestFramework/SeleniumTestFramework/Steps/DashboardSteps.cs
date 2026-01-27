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
        private DashboardPage _dashboardPage;

        public DashboardSteps(IWebDriver webDriver, SettingsModel model, DashboardPage dashboardPage)
        {
            this._driver = webDriver;
            this._settingsModel = model;
            _dashboardPage = dashboardPage;
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
