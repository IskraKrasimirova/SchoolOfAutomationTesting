using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class RegisterSteps
    {
        private readonly IWebDriver _driver;
        private readonly SettingsModel _settingsModel;
        private readonly ScenarioContext _scenarioContext;
        private RegisterPage _registerPage;

        public RegisterSteps(IWebDriver driver, SettingsModel settingsModel, ScenarioContext scenarioContext, RegisterPage _registerPage)
        {
            this._driver = driver;
            this._settingsModel = settingsModel;
            this._scenarioContext = scenarioContext;
            this._registerPage = _registerPage;
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
    }
}
