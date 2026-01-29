using Reqnroll;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class UsersSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly UsersPage _usersPage;

        public UsersSteps(ScenarioContext scenarioContext, UsersPage usersPage)
        {
            _scenarioContext = scenarioContext;
            _usersPage = usersPage;
        }

        [When("I delete the created user")]
        public void WhenIDeleteTheCreatedUser()
        {
            _usersPage.VerifyIsAtUsersPage(true);
            var user = _scenarioContext.Get<RegisterModel>("RegisteredUser");
            _usersPage.VerifyUserExists(user.Email);

            _usersPage.DeleteUser(user.Email);
            _usersPage.VerifyUserDoesNotExist(user.Email);
        }

        [Then("the new user should be present in the users list")]
        public void ThenTheNewUserShouldBePresentInTheUsersList()
        {
            _usersPage.VerifyIsAtUsersPage(true);
            var newUser = _scenarioContext.Get<RegisterModel>("RegisteredUser");
            _usersPage.VerifyUserExists(newUser.Email);
        }

        [Then("the user should no longer be present in the users list")]
        public void ThenTheUserShouldNoLongerBePresentInTheUsersList()
        {
            //var newUser = (RegisterModel)_scenarioContext["RegisteredUser"];
            var newUser = _scenarioContext.Get<RegisterModel>("RegisteredUser");
            _usersPage.VerifyUserDoesNotExist(newUser.Email);
        }
    }
}
