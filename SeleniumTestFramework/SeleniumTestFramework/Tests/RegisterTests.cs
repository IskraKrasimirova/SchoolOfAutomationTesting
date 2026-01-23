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
        private RegisterPage _registerPage;

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("http://localhost:8080/register.php");
            _registerPage = new RegisterPage(_driver);
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

            _registerPage.RegisterNewUser(newUser);

            var homePage = new HomePage(_driver);

            var emailDropdownText = homePage.GetLoggedUserEmail();
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

        [Test]
        public void RegistrationWith_EmptyUserData_ShowsErrorMessages()
        {
            var newUser = new RegisterModel("Mr.", "", "", "", "", "", "", false);
            // "Mr." is a default title in dropdown. The title cannot be empty due to the structure of HTML.
            // So, the error message for title will never appear.
            _registerPage.RegisterNewUser(newUser);
            var firstNameValidationMessage = _registerPage.GetFirstNameValidationMessage();
            var surnameValidationMessage = _registerPage.GetSurnameValidationMessage();
            var emailValidationMessage = _registerPage.GetEmailValidationMessage();
            var passwordValidationMessage = _registerPage.GetPasswordValidationMessage();
            var countryValidationMessage = _registerPage.GetCountryValidationMessage();
            var cityValidationMessage = _registerPage.GetCityValidationMessage();
            var agreementValidationMessage = _registerPage.GetAgreementValidationMessage();

            Assert.Multiple(() =>
            {
                Assert.That(firstNameValidationMessage, Is.EqualTo("Please enter a valid first name (letters only, 2-15 characters)."), "First name validation message is incorrect.");
                Assert.That(surnameValidationMessage, Is.EqualTo("Please enter a valid surname (letters only, 2-15 characters)."), "Surname validation message is incorrect.");
                Assert.That(emailValidationMessage, Is.EqualTo("Please enter a valid email address."), "Email validation message is incorrect.");
                Assert.That(passwordValidationMessage, Is.EqualTo("Password must be at least 6 characters long."), "Password validation message is incorrect.");
                Assert.That(countryValidationMessage, Is.EqualTo("Please enter your country."), "Country validation message is incorrect.");
                Assert.That(cityValidationMessage, Is.EqualTo("Please enter your city."), "City validation message is incorrect.");
                Assert.That(agreementValidationMessage, Is.EqualTo("You must agree to the terms of service."), "Agreement validation message is incorrect.");
            });
        }
    }
}
