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
    [TestFixture(Category = "Register")]
    public class RegisterTests
    {
        private IWebDriver _driver;
        private RegisterPage _registerPage;
        private readonly SettingsModel _settingsModel;
        private static readonly string[] _titles = ["Mr.", "Mrs."];
        private static readonly string[] _cities = ["Burgas", "Elin Pelin", "Kardjali", "Pleven", "Plovdiv", "Pravets", "Sofia", "Sopot", "Varna"];

        public RegisterTests()
        {
            _settingsModel = ConfigurationManager.Instance.SettingsModel;
        }

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl($"{_settingsModel.BaseUrl}register.php");
            _registerPage = new RegisterPage(_driver);
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void RegistrationWith_ValidUserData_LogsUserIn_AndShowsDashboardPage()
        {
            var faker = new Faker();

            var newUser = new RegisterModel
            (
                faker.PickRandom(_titles),
                faker.Name.FirstName(),
                faker.Name.LastName(),
                faker.Internet.Email(),
                faker.Internet.Password(),
                "Bulgaria",
                faker.PickRandom(_cities),
                true
            );

            _registerPage.RegisterNewUser(newUser);

            var dashboardPage = new DashboardPage(_driver);
            dashboardPage.VerifyIsAtDashboardPage();
            dashboardPage.VerifyUserIsLoggedIn(newUser.Email, $"{newUser.FirstName} {newUser.Surname}", false);
        }

        [Test]
        public void RegistrationWith_EmptyUserData_ShowsErrorMessages()
        {
            var newUser = new RegisterModel("Mr.", "", "", "", "", "", "", false);
            // "Mr." is a default title in dropdown. The title cannot be empty due to the structure of HTML.
            // So, the error message for title will never appear. Issue!
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

        [Test]
        public void RegistrationWith_ExistingEmail_ShowsErrorMessage()
        {
            var faker = new Faker();

            var newUser = new RegisterModel
            (
                faker.PickRandom(_titles),
                faker.Name.FirstName(),
                faker.Name.LastName(),
                _settingsModel.Email,
                faker.Internet.Password(),
                "Bulgaria",
                faker.PickRandom(_cities),
                true
            );

            _registerPage.RegisterNewUser(newUser);
            _registerPage.VerifyPasswordInputIsEmpty();
            _registerPage.VerifyGlobalAlertMessage("User with such email already exists");
        }

        // Backend has a hidden 15-character limit for city names. 
        // When the city exceeds 15 characters, the server returns a PHP warning instead of 
        // the expected validation message. The frontend displays the warning directly, 
        // so the correct message never appears.
        // For example, "Gorno Draglishte" has 16 characters, and is a valid city in Bulgaria. But the test fails with error message: Warning: Array to string conversion in /var/www/html/register.php on line 122 Array
        // Another examples: "Sofia City Center" has 17 characters and the same issue occurs.
        // "InvalidCityName123" has 18 characters and the same issue occurs.
        // "Novo Selo Vidin" has 15 characters and works fine.
        // Invalid city name with 1 character has the same issue.
        // So, there is a backend validation for city length between 2 and 15 characters, but it is not shown with clear message in UI.
        // The same hidden 2–15 character length validation applies to the Country field. 
        // Values shorter than 2 or longer than 15 characters cause the backend to return 
        // a PHP warning instead of a proper validation message, and the UI does not show 
        // a clear error to the user.
        [Test]
        [Category("BackendIssue")]
        public void RegistrationWith_NotValidCityForCountry_ShowsErrorMessage()
        {
            var faker = new Faker();

            var newUser = new RegisterModel
            (
                faker.PickRandom(_titles),
                faker.Name.FirstName(),
                faker.Name.LastName(),
                faker.Internet.Email(),
                faker.Internet.Password(),
                "Bulgaria",
                "New York",
                true
            );

            _registerPage.RegisterNewUser(newUser);

            Retry.Until(() =>
            {
                if (!_registerPage.IsPasswordInputEmpty())
                    throw new RetryException("Password input is not empty yet.");
            });

            _registerPage.VerifyPasswordInputIsEmpty();

            Retry.Until(() =>
            {
                var errorMessage = _registerPage.GetGlobalAlertMessage();
                if (errorMessage != "City does not belong to the specified country")
                    throw new RetryException($"Still waiting for correct validation message. Current: {errorMessage}");
            }, retryNumber: 5, waitInMilliseconds: 5000);

            //_driver.WaitUntilTextIsPresent(_registerPage.AlertElement, "City does not belong to the specified country");

            var errorMessage = _registerPage.GetGlobalAlertMessage();
            Console.WriteLine($"ERROR: {errorMessage}");

            Assert.That(errorMessage, Does.Not.Contain("Warning"), "Backend returned a PHP warning instead of a proper validation message.");
            Assert.That(errorMessage, Is.EqualTo("City does not belong to the specified country"), "Expected city-country validation message was not shown.");
        }
    }
}