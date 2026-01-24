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
    public class UsersTests
    {
        private IWebDriver _driver;
        private readonly SettingsModel _settingsModel;

        public UsersTests()
        {
            _settingsModel = ConfigurationManager.Instance.SettingsModel;
        }

        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_settingsModel.BaseUrl);
        }

        [TearDown]
        public void Teardown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Test]
        public void UserCanRegister_AndAdminCanDeleteUser()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.GoToRegisterPage();

            Assert.That(_driver.Url, Does.Contain("/register"), "Did not navigate to Register page.");

            var registerPage = new RegisterPage(_driver);

            var faker = new Faker("en");

            var newUser = new RegisterModel
            (
                faker.PickRandom(new[] { "Mr.", "Mrs." }),
                faker.Name.FirstName(),
                faker.Name.LastName(),
                faker.Internet.Email(),
                faker.Internet.Password(),
                "Bulgaria",
                faker.PickRandom(new[] { "Burgas", "Elin Pelin", "Kardjali", "Pleven", "Plovdiv", "Pravets", "Sofia", "Sopot", "Varna" }),
                true
            );

            registerPage.RegisterNewUser(newUser);

            Assert.That(_driver.Url, Does.Contain("index.php"), "Registration did not redirect to dashboard.");

            var dashboardPage = new DashboardPage(_driver);
            dashboardPage.VerifyUserIsLoggedIn(newUser.Email, $"{newUser.FirstName} {newUser.Surname}", false);
            
            dashboardPage.Logout();
            Assert.That(loginPage.IsAtLoginPage(), "Logout did not redirect to login page.");

            loginPage.LoginWith(_settingsModel.Email, _settingsModel.Password);
            dashboardPage.VerifyUserIsLoggedIn(_settingsModel.Email, _settingsModel.Username, true);
            
            var usersPage = dashboardPage.GoToUsersPage();
            Assert.That(usersPage.IsAtUsersPage(), "Users page did not load correctly.");
            Assert.That(usersPage.FindUserRowByEmail(newUser.Email), Is.Not.Null, "User not found.");

            usersPage.DeleteUser(newUser.Email); 
            Assert.That(usersPage.FindUserRowByEmail(newUser.Email), Is.Null, "User still present after deletion.");

            dashboardPage.Logout();
            Assert.That(loginPage.IsAtLoginPage(), "Logout did not redirect to login page.");

            loginPage.LoginWith(newUser.Email, newUser.Password);

            loginPage.VerifyPasswordInputIsEmpty();
            loginPage.VerifyErrorMessageIsDisplayed("Invalid email or password");
        }
    }
}
