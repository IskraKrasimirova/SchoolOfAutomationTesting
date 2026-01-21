using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTestFramework.Pages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTestFramework.Tests
{
    public class LoginTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://localhost:8080");
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        [TestCaseSource(nameof(ValidLoginData))]
        public void LoginWith_ValidUserCredentials_ShouldBeSuccsessful(string email, string password, bool isAdmin)
        {
            var loginPage = new LoginPage(_driver);

            // Assert we are on the correct page BEFORE interacting
            Assert.That(_driver.Url, Does.Contain("/login"), "Login page did not load correctly.");

            loginPage.LoginWith(email, password);

            var homePage = new HomePage(_driver);

            var emailDropdownText = homePage.GetEmailElementText();
            Assert.That(emailDropdownText, Is.EqualTo(email), "User email is not shown.");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(homePage.IsHomeLinkDisplayed(), "Home link is not displayed.");
                Assert.IsTrue(homePage.IsUsersLinkDisplayed(), "Users link is not displayed.");
                Assert.IsTrue(homePage.IsSearchLinkDisplayed(), "Search link is not displayed.");
            });

            if (isAdmin)
            {
                Assert.IsTrue(homePage.IsAddUserLinkDisplayed(), "Add User link should be visible for admin.");
            }
            else
            {
                Assert.IsFalse(homePage.IsAddUserLinkDisplayed(), "Add User link should NOT be visible for common user.");
            }
        }

        private static IEnumerable<TestCaseData> ValidLoginData()
        {
            yield return new TestCaseData("admin@automation.com", "pass123", true);
            yield return new TestCaseData("idimitrov@automation.com", "pass123", false);
        }

        [Test]
        [TestCaseSource(nameof(NotValidLoginData))]
        public void LoginWith_NotValidUserCredentials_ShowsValidationMessage(string testedCase, string email, string password)
        {
            var loginPage = new LoginPage(_driver);
            // Assert we are on the correct page BEFORE interacting
            Assert.That(_driver.Url, Does.Contain("/login"), "Login page did not load correctly.");

            loginPage.LoginWith(email, password);

            var errorDialogText = loginPage.GetValidationMessage();
            Assert.That(errorDialogText, Is.EqualTo("Invalid email or password"));
            Assert.IsTrue(loginPage.IsPasswordInputEmpty(), "Password input should be cleared after failed login attempt.");
        }

        private static IEnumerable<TestCaseData> NotValidLoginData()
        {
            yield return new TestCaseData("Wrong password", "admin@automation.com", "password");
            yield return new TestCaseData("Wrong email", "admin@admin.com", "pass123");
            yield return new TestCaseData("Wrong email and password", "a@a.com", "wrongpassword");
        }

        [Test]
        public void LoginWith_NonExistingUser_ShowsValidationMessage()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.LoginWith("user@test.com", "wrongpass");

            loginPage.VerifyPasswordInputIsEmpty();

            var errorDialogText = _driver.FindElement(By.ClassName("alert")).Text;
            Assert.That(errorDialogText, Is.EqualTo("Invalid email or password"));
        }

        [Test]
        [TestCaseSource(nameof(InvalidEmailFormat))]
        public void LoginWith_InvalidEmailFormat_ShowsBrowserValidationMessage(string email, string password, string message)
        {
            var loginPage = new LoginPage(_driver);
            // Assert we are on the correct page BEFORE interacting
            Assert.That(_driver.Url, Does.Contain("/login"), "Login page did not load correctly.");

            loginPage.LoginWith(email, password);

            var validationMessage = loginPage.GetEmailBrowserValidationMessage();

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
            var loginPage = new LoginPage(_driver);
            loginPage.LoginWith("admin@automation.com", "123");

            var message = loginPage.GetPasswordValidationMessage();

            Assert.That(message, Is.EqualTo("Password must be at least 6 characters"));
        }

        [Test]
        public void LoginWith_EmptyPassword_ShowsBrowserValidationMessage()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.LoginWith("admin@automation.com", "123");

            var message = loginPage.GetPasswordBrowserValidationMessage();

            Assert.That(message, Is.EqualTo("Please fill out this field."));
        }
    }
}