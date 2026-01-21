using SoAAutomationFramework.Models;
using SoAAutomationFramework.Pages;
using SoAAutomationFramework.Utils;

namespace SoAAutomationFramework.Tests
{
    [Category("Login")]
    public class LoginTests
    {
        private LoginPage _loginPage;

        [SetUp]
        public void Setup()
        {
            WebDriverProvider.InitDriver();
            _loginPage = new LoginPage();
            _loginPage.StartApplication();
        }

        [TearDown]
        public void TearDown()
        {
            _loginPage.Driver.Quit();    
        }

        [Test]
        [TestCaseSource(nameof(ValidLoginData))]
        public void LoginWith_ValidUserCredentials_ShouldBeSuccsessful(LoginModel loginModel, bool isAdmin)
        {
            _loginPage.OpenPage("Login");

            // Assert we are on the correct page BEFORE interacting
            Assert.That(_loginPage.Driver.Url.Contains("/login"), "Login page did not load correctly.");

            _loginPage.Login(loginModel);

            var homePage = new HomePage();

            var emailDropdownText = homePage.GetEmailElementText();
            Assert.That(emailDropdownText, Is.EqualTo(loginModel.Email), "User email is not shown.");

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
            yield return new TestCaseData(
                new LoginModel("admin@automation.com", "pass123"), true
            );
            yield return new TestCaseData(
                new LoginModel("idimitrov@automation.com", "pass123"), false
            );
        }

        [Test]
        public void LoginWith_NonExistingUser_ShowsValidationMessage()
        {
            _loginPage.OpenPage("Login");
            // Assert we are on the correct page BEFORE interacting
            Assert.That(_loginPage.Driver.Url, Does.Contain("/login"), "Login page did not load correctly.");

            var invalidLoginModel = new LoginModel("a@a.com", "wrongpassword");
            _loginPage.Login(invalidLoginModel);

            var errorDialogText = _loginPage.GetValidationMessage();
            Assert.That(errorDialogText, Is.EqualTo("Invalid email or password"));
            Assert.IsTrue(_loginPage.IsPasswordInputEmpty(), "Password input should be cleared after failed login attempt.");
        }

        [Test]
        [TestCaseSource(nameof(InvalidEmailFormat))]
        public void LoginWith_InvalidEmailFormat_ShowsBrowserValidationMessage(LoginModel loginModel, string message)
        {
            _loginPage.OpenPage("Login");
            _loginPage.Login(loginModel);
            var validationMessage = _loginPage.GetEmailBrowserValidationMessage();

            Assert.That(validationMessage, Does.Contain(message));
        }

        private static IEnumerable<TestCaseData> InvalidEmailFormat()
        {
            yield return new TestCaseData(new LoginModel("abc", "pass123"), "missing an '@'");
            yield return new TestCaseData(new LoginModel("abc@", "pass123"), "incomplete");
            yield return new TestCaseData(new LoginModel("abc abc@test.com", "pass123"), "should not contain the symbol ' '");
            yield return new TestCaseData(new LoginModel("@test", "pass123"), "incomplete");
            yield return new TestCaseData(new LoginModel("@test.com", "pass123"), "incomplete");
            yield return new TestCaseData(new LoginModel("abc@@test.com", ""), "should not contain the symbol '@'");
            yield return new TestCaseData(new LoginModel("abc@.test.com", ""), "a wrong position");
            yield return new TestCaseData(new LoginModel("abc@test .com", ""), "should not contain the symbol ' '");
            yield return new TestCaseData( new LoginModel("", ""), "fill out this field");
        }
        // Exact error messages:
        // "Please include an '@' in the email address. 'abc' is missing an '@'."
        // "Please enter a part following '@'. 'abc@' is incomplete."
        // "A part followed by '@' should not contain the symbol ' '."
        // "Please enter a part followed by '@'. '@test' is incomplete."
        // "Please enter a part followed by '@'. '@test.com' is incomplete."
        // "A part following '@' should not contain the symbol '@'."
        // "'.' is used at a wrong position in '.test.com'."
        // "A part following '@' should not contain the symbol ' '."
        // "Please fill out this field."

        [Test]
        public void LoginWith_ShortPassword_ShowsValidationMessage()
        {
            _loginPage.OpenPage("Login");

            var model = new LoginModel("admin@automation.com", "123");
            _loginPage.Login(model);

            var message = _loginPage.GetPasswordValidationMessage();

            Assert.That(message, Is.EqualTo("Password must be at least 6 characters"));
        }

        [Test]
        public void LoginWith_EmptyPassword_ShowsBrowserValidationMessage()
        {
            _loginPage.OpenPage("Login");

            var model = new LoginModel("admin@automation.com", "");
            _loginPage.Login(model);

            var message = _loginPage.GetPasswordBrowserValidationMessage();

            Assert.That(message, Is.EqualTo("Please fill out this field."));
        }
    }
}