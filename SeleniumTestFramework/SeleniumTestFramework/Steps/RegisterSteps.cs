using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class RegisterSteps
    {
        private readonly IWebDriver _driver;
        private readonly SettingsModel _settingsModel;
        private readonly ScenarioContext _scenarioContext;
        private RegisterPage _registerPage;
        private DashboardPage _dashboardPage;
        private LoginPage _loginPage;

        public RegisterSteps(IWebDriver driver, SettingsModel settingsModel, ScenarioContext scenarioContext, RegisterPage _registerPage, DashboardPage dashboardPage, LoginPage loginPage)
        {
            this._driver = driver;
            this._settingsModel = settingsModel;
            this._scenarioContext = scenarioContext;
            this._registerPage = _registerPage;
            this._dashboardPage = dashboardPage;
            this._loginPage = loginPage;
        }

        [When("I verify that the registration form is displayed")]
        public void WhenIVerifyThatTheRegistrationFormIsDisplayed()
        {
            _registerPage.VerifyIsAtRegisterPage();
        }

        [When("I register a new user with valid details")]
        public void WhenIRegisterANewUserWithValidDetails()
        {
            var newUser = UserFactory.CreateValidUser();
            _registerPage.RegisterNewUser(newUser);
            _scenarioContext["RegisteredUser"] = newUser;
        }

        [When("I register a new user with valid details and log out")]
        public void WhenIRegisterANewUserWithValidDetailsAndLogOut()
        {
            _loginPage.GoToRegisterPage();

            _registerPage.VerifyIsAtRegisterPage();
            var newUser = UserFactory.CreateValidUser();
            _registerPage.RegisterNewUser(newUser);
            _scenarioContext["RegisteredUser"] = newUser;

            _dashboardPage.VerifyIsAtDashboardPage();
            _dashboardPage.VerifyUserIsLoggedIn(newUser.Email, $"{newUser.FirstName} {newUser.Surname}", false);

            _dashboardPage.Logout();
        }
    }
}
