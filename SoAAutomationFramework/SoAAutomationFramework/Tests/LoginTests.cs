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
    }
}