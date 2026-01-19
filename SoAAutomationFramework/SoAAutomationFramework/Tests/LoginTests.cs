using SoAAutomationFramework.Pages;
using SoAAutomationFramework.Utils;

namespace SoAAutomationFramework.Tests
{
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
        public void LoginPageLoadsTest()
        {
            _loginPage.OpenPage("Login");
            Assert.IsTrue(_loginPage.Driver.Url.Contains("/login"), "URL is incorrect.");
        }

        [Test]
        public void LoginWith_ValidAdminCredentials_ShouldBeSuccsessful()
        {
            _loginPage.OpenPage("Login");
            _loginPage.Login("admin@automation.com", "pass123");

            var emailDropdownText = _loginPage.GetEmailElementText();
            Assert.That(emailDropdownText, Is.EqualTo("admin@automation.com"), "Admin email is not shown.");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(_loginPage.IsHomeLinkDisplayed(), "Home link is not displayed.");
                Assert.IsTrue(_loginPage.IsUsersLinkDisplayed(), "Users link is not displayed.");
                Assert.IsTrue(_loginPage.IsSearchLinkDisplayed(), "Search link is not displayed.");
            });
        }
    }
}