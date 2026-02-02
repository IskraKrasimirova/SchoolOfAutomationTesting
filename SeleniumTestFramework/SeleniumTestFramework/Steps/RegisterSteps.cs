using Reqnroll;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities;
using SeleniumTestFramework.Utilities.Constants;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class RegisterSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private RegisterPage _registerPage;
        private DashboardPage _dashboardPage;
        private LoginPage _loginPage;

        public RegisterSteps(ScenarioContext scenarioContext, RegisterPage _registerPage, DashboardPage dashboardPage, LoginPage loginPage)
        {
            this._scenarioContext = scenarioContext;
            this._registerPage = _registerPage;
            this._dashboardPage = dashboardPage;
            this._loginPage = loginPage;
        }

        [Given("I register a new user")]
        public void GivenIRegisterANewUser()
        {
            _loginPage.GoToRegisterPage();

            _registerPage.VerifyIsAtRegisterPage();
            var newUser = UserFactory.CreateValidUser();
            _registerPage.RegisterNewUser(newUser);
            _scenarioContext.Add(ContextConstants.RegisteredUser, newUser);

            _dashboardPage.VerifyIsAtDashboardPage();
            _dashboardPage.VerifyUserIsLoggedIn(newUser.Email, $"{newUser.FirstName} {newUser.Surname}", false);

            _dashboardPage.Logout();
        }

        [When("I verify that the registration form is displayed")]
        public void WhenIVerifyThatTheRegistrationFormIsDisplayed()
        {
            _registerPage.VerifyIsAtRegisterPage();
        }

        [When("I register a new user with valid details")]
        public void WhenIRegisterANewUserWithValidDetails()
        {
            var newUser = UserFactory.CreateValidUser();
            _registerPage.RegisterNewUser(newUser);
            _scenarioContext.Add(ContextConstants.RegisteredUser, newUser);
        }
    }
}
