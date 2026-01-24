using Bogus;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTestFramework.Tests
{
    public class LoginTests
    {
        private IWebDriver _driver;
        private LoginPage _loginPage;
        private readonly SettingsModel _settingsModel;

        public LoginTests()
        {
            _settingsModel = ConfigurationManager.Instance.SettingsModel;
        }

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            _driver.Navigate().GoToUrl(_settingsModel.BaseUrl);
            _loginPage = new LoginPage(_driver);
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        [Category("FromLiveSession")]
        public void LoginWith_ExistingUser_ShouldShowTheDashboard()
        {
            _loginPage.LoginWith(_settingsModel.Email, _settingsModel.Password);

            var dashboardPage = new DashboardPage(_driver);
            dashboardPage.VerifyLoggedUserEmailIs(_settingsModel.Email);
            dashboardPage.VerifyUsernameIs(_settingsModel.Username);
        }

        [Test]
        [TestCaseSource(nameof(ValidLoginData))]
        public void LoginWith_ValidUserCredentials_ShouldShowTheDashboard(string email, string password, string username, bool isAdmin)
        {
            // Assert we are on the correct page BEFORE interacting
            Assert.That(_loginPage.IsAtLoginPage(), "Login page did not load correctly.");

            _loginPage.LoginWith(email, password);

            var dashboardPage = new DashboardPage(_driver);

            dashboardPage.VerifyUserIsLoggedIn(email, username, isAdmin);
        }

        private static IEnumerable<TestCaseData> ValidLoginData()
        {
            var settingsModel = ConfigurationManager.Instance.SettingsModel;

            yield return new TestCaseData(settingsModel.Email, settingsModel.Password, settingsModel.Username, true);
            yield return new TestCaseData("idimitrov@automation.com", "pass123", "Ivan Dimotrov", false);
        }

        [Test]
        [TestCaseSource(nameof(NotValidLoginData))]
        public void LoginWith_NotValidUserCredentials_ShowsValidationMessage(string testedCase, string email, string password)
        {
            // Assert we are on the correct page BEFORE interacting
            Assert.That(_loginPage.IsAtLoginPage(), "Login page did not load correctly.");

            _loginPage.LoginWith(email, password);

            _loginPage.VerifyPasswordInputIsEmpty();
            _loginPage.VerifyErrorMessageIsDisplayed("Invalid email or password");

            //Assert.That(_loginPage.IsPasswordInputEmpty(), "Password input should be cleared after failed login attempt.");

            //var errorDialogText = _loginPage.GetValidationMessage();
            //Assert.That(errorDialogText, Is.EqualTo("Invalid email or password")); 
        }

        private static IEnumerable<TestCaseData> NotValidLoginData()
        {
            yield return new TestCaseData("Wrong password", "admin@automation.com", "password");
            yield return new TestCaseData("Wrong email", "admin@admin.com", "pass123");
            yield return new TestCaseData("Wrong email and password", "a@a.com", "wrongpassword");
        }

        [Test]
        [Category("FromLiveSession")]
        public void LoginWith_NonExistingUser_ShowsValidationMessage()
        {
            var faker = new Faker();
            _loginPage.LoginWith(faker.Internet.Email(), faker.Internet.Password());

            Retry.Until(() =>
            {
                if (!_loginPage.IsPasswordInputEmpty())
                    throw new RetryException("Password input is not empty yet.");
            });

            _loginPage.VerifyPasswordInputIsEmpty();
            _loginPage.VerifyErrorMessageIsDisplayed("Invalid email or password");
        }

        [Test]
        [TestCaseSource(nameof(InvalidEmailFormat))]
        public void LoginWith_InvalidEmailFormat_ShowsBrowserValidationMessage(string email, string password, string message)
        {
            // Assert we are on the correct page BEFORE interacting
            Assert.That(_loginPage.IsAtLoginPage(), "Login page did not load correctly.");

            _loginPage.LoginWith(email, password);

            var validationMessage = _loginPage.GetEmailBrowserValidationMessage();

            Assert.That(validationMessage, Does.Contain(message));
        }

        private static IEnumerable<TestCaseData> InvalidEmailFormat()
        {
            yield return new TestCaseData("abc", "pass123", "missing an '@'");
            yield return new TestCaseData("abc@", "pass123", "incomplete");
            yield return new TestCaseData("abc abc@test.com", "pass123", "should not contain the symbol ' '");
            yield return new TestCaseData("@test", "pass123", "incomplete");
            yield return new TestCaseData("@test.com", "pass123", "incomplete");
            yield return new TestCaseData("abc@@test.com", "", "should not contain the symbol '@'");
            yield return new TestCaseData("abc@.test.com", "", "a wrong position");
            yield return new TestCaseData("abc@test .com", "", "should not contain the symbol ' '");
            yield return new TestCaseData("", "", "fill out this field");
        }

        [Test]
        public void LoginWith_ShortPassword_ShowsValidationMessage()
        {
            Assert.That(_loginPage.IsAtLoginPage(), "Login page did not load correctly.");

            _loginPage.LoginWith("admin@automation.com", "123");

            var message = _loginPage.GetPasswordValidationMessage();

            Assert.That(message, Is.EqualTo("Password must be at least 6 characters"));
        }

        [Test]
        public void LoginWith_EmptyPassword_ShowsBrowserValidationMessage()
        {
            Assert.That(_loginPage.IsAtLoginPage(), "Login page did not load correctly.");

            _loginPage.LoginWith("admin@automation.com", "");

            var message = _loginPage.GetPasswordBrowserValidationMessage();

            Assert.That(message, Is.EqualTo("Please fill out this field."));
        }
    }
}