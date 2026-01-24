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
            var registerPage = loginPage.GoToRegisterPage();
            registerPage.VerifyIsAtRegisterPage();

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

            var dashboardPage = new DashboardPage(_driver);
            dashboardPage.VerifyIsAtDashboardPage();
            dashboardPage.VerifyUserIsLoggedIn(newUser.Email, $"{newUser.FirstName} {newUser.Surname}", false);
            
            dashboardPage.Logout();
            loginPage.VerifyIsAtLoginPage();

            loginPage.LoginWith(_settingsModel.Email, _settingsModel.Password);
            dashboardPage.VerifyUserIsLoggedIn(_settingsModel.Email, _settingsModel.Username, true);
            
            var usersPage = dashboardPage.GoToUsersPage();
            usersPage.VerifyIsAtUsersPage(true);
            usersPage.VerifyUserExists(newUser.Email);

            usersPage.DeleteUser(newUser.Email); 
            usersPage.VerifyUserDoesNotExist(newUser.Email);

            dashboardPage.Logout();
            loginPage.VerifyIsAtLoginPage();

            loginPage.LoginWith(newUser.Email, newUser.Password);

            loginPage.VerifyPasswordInputIsEmpty();
            loginPage.VerifyErrorMessageIsDisplayed("Invalid email or password");
        }
    }
}
