using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly IWebDriver _driver;
        private readonly SettingsModel _settingsModel;
        private readonly ScenarioContext _scenarioContext;
        private LoginPage _loginPage;

        public LoginSteps(IWebDriver driver, SettingsModel model, ScenarioContext scenarioContex, LoginPage loginPage)
        {
            this._driver = driver;
            this._settingsModel = model;
            this._scenarioContext = scenarioContex;
            this._loginPage = loginPage;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            //_loginPage = new LoginPage(_driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //_driver.Quit();
            //_driver.Dispose();
            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Navigate().GoToUrl(_settingsModel.BaseUrl);
        }

        [Given("I navigate to the main page")]
        public void GivenINavigateToTheMainPage()
        {
            _driver.Navigate().GoToUrl(_settingsModel.BaseUrl);
        }

        [Given("I verify that the login form is displayed")]
        public void GivenIVerifyThatTheLoginFormIsDisplayed()
        {
            _loginPage.VerifyIsAtLoginPage();
        }

        [When("I login with valid credentials")]
        public void WhenILoginWithValidCredentials()
        {
            _loginPage.LoginWith(_settingsModel.Email, _settingsModel.Password);
        }

        [When("I login with invalid credentials")]
        public void WhenILoginWithInvalidCredentials()
        {
            _loginPage.LoginWith("notexistinguser@gmail.com", _settingsModel.Password);
        }

        [When("I login with {string} and {string}")]
        public void WhenILoginWithAnd(string email, string password)
        {
            if (email == "readFromSettings")
            {
                WhenILoginWithValidCredentials();
            }
            else
            {
                _loginPage.LoginWith(email, password);
            }
        }

        [When("the administrator logs in with valid credentials")]
        public void WhenTheAdministratorLogsInWithValidCredentials()
        {
            WhenILoginWithValidCredentials();
        }

        [When("I login with the deleted user's credentials")]
        public void WhenILoginWithTheDeletedUsersCredentials()
        {
            var deletedUser = (RegisterModel)_scenarioContext["RegisteredUser"];
            _loginPage.LoginWith(deletedUser.Email, deletedUser.Password);
            //WhenILoginWithAnd(deletedUser.Email, deletedUser.Password);
        }

        [When("I navigate to the main page")]
        public void WhenINavigateToTheMainPage()
        {
            GivenINavigateToTheMainPage();
        }

        [When("I verify that the login form is displayed")]
        public void WhenIVerifyThatTheLoginFormIsDisplayed()
        {
            GivenIVerifyThatTheLoginFormIsDisplayed();
        }

        [Then("I should still be on the login page")]
        public void ThenIShouldStillBeOnTheLoginPage()
        {
            Assert.That(_driver.Url, Is.EqualTo(_settingsModel.BaseUrl + "login.php"));
        }

        [Then("I should see an error message with the following text {string}")]
        public void ThenIShouldSeeAnErrorMessageWithTheFollowingText(string errorText)
        {
            Retry.Until(() =>
            {
                if (!_loginPage.IsPasswordInputEmpty())
                    throw new RetryException("Password input is not empty yet.");
            });

            _loginPage.VerifyPasswordInputIsEmpty();
            _loginPage.VerifyErrorMessageIsDisplayed(errorText);
        }
    }
}
