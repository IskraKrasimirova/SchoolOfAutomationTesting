using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private readonly SettingsModel _settingsModel;

        public LoginSteps()
        {
            _settingsModel = ConfigurationManager.Instance.SettingsModel;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _loginPage = new LoginPage(_driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
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

        [Then("I should see the logged user in the main header")]
        public void ThenIShouldSeeTheLoggedUserInTheMainHeader()
        {
            var dashboardPage = new DashboardPage(_driver);
            dashboardPage.VerifyLoggedUserEmailIs(_settingsModel.Email);
            dashboardPage.VerifyUsernameIs(_settingsModel.Username);
        }

        [Then("I should be able to logout successfully")]
        public void ThenIShouldBeAbleToLogoutSuccessfully()
        {
            var dashboardPage = new DashboardPage(_driver);
            dashboardPage.Logout();
        }
    }
}
