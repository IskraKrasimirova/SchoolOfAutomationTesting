using Reqnroll;
using SeleniumTestFramework.DatabaseOperations.Operations;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities;
using SeleniumTestFramework.Utilities.Constants;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class RegisterSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly RegisterPage _registerPage;
        private readonly DashboardPage _dashboardPage;
        private readonly LoginPage _loginPage;
        private readonly UserOperations _userOperations;

        public RegisterSteps(ScenarioContext scenarioContext, RegisterPage _registerPage, DashboardPage dashboardPage, LoginPage loginPage, UserOperations userOperations)
        {
            this._scenarioContext = scenarioContext;
            this._registerPage = _registerPage;
            this._dashboardPage = dashboardPage;
            this._loginPage = loginPage;
            this._userOperations = userOperations;
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

        [Given("I register new user with valid details")]
        public void GivenIRegisterNewUserWithValidDetails()
        {
            _loginPage.VerifyIsAtLoginPage();

            _loginPage.GoToRegisterPage();
            _registerPage.VerifyIsAtRegisterPage();

            var newUser = UserFactory.CreateValidUser();
            _registerPage.RegisterNewUser(newUser);
            _scenarioContext.Add(ContextConstants.RegisteredUser, newUser);

            Retry.Until(() =>
            {
                var doesUserExist = _userOperations.CheckIfUserExistsByEmail(newUser.Email);
                if (doesUserExist == false)
                    throw new RetryException("Registerd User is not found in the database.");
            });
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
