using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTestFramework.Tests
{
    public class RegisterTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://localhost:8080/register.php");
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void RegistrationWith_ValidUserData_LogsUserIn_AndShowsHomePage()
        {
            RegisterPage registerPage = new RegisterPage(_driver);
            var newUser = new RegisterModel
            (
                "Mr.",
                "John",
                "Doe",
                $"user{Guid.NewGuid().ToString("N").Substring(0, 6)}@test.com",
                "secretpass",
                "USA",
                "New York",
                true
            );

            registerPage.RegisterNewUser(newUser);

            var homePage = new HomePage(_driver);

            var emailDropdownText = homePage.GetEmailElementText();
            Assert.That(emailDropdownText, Is.EqualTo(newUser.Email), "User email is not shown.");

            var greetingText = homePage.GetGreetingText();
            var expectedGreeting = $"Hello, {newUser.Title} {newUser.FirstName} {newUser.Surname}";
            Assert.That(greetingText, Is.EqualTo(expectedGreeting), "Greeting text is incorrect.");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(homePage.IsHomeLinkDisplayed(), "Home link is not displayed.");
                Assert.IsTrue(homePage.IsUsersLinkDisplayed(), "Users link is not displayed.");
                Assert.IsTrue(homePage.IsSearchLinkDisplayed(), "Search link is not displayed.");
            });
        }
    }
}
