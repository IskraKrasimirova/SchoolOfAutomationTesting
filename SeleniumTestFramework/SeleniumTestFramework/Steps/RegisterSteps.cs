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

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver.Navigate().GoToUrl($"{_settingsModel.BaseUrl}register.php");
        }

        [Given("a new user can register with valid data successfully")]
        public void GivenANewUserCanRegisterWithValidDataSuccessfully()
        {
            var newUser = UserFactory.CreateValidUser();
            _registerPage.RegisterNewUser(newUser);
            _scenarioContext["RegisteredUser"] = newUser;
        }

    }
}
