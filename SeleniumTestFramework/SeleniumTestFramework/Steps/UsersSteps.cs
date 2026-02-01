using Reqnroll;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities;

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

        [Given("I open the Add New User form")]
        public void GivenIOpenTheAddNewUserForm()
        {
            var addUserModal = _usersPage.OpenAddUserModal();
            addUserModal.VerifyIsAtAddUserModal();
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

        [When("I add a new user with valid details")]
        public void WhenIAddANewUserWithValidDetails()
        {
            var modal = _usersPage.OpenAddUserModal();
            modal.VerifyIsAtAddUserModal();

            var newUser = UserFactory.CreateValidCommonUser();
            modal.AddUser(newUser);

            _scenarioContext.Add("AddedUser", newUser);

            modal.VerifyModalIsClosed();

            _usersPage.VerifyIsAtUsersPage(true);
            _usersPage.VerifyUserExists(newUser.Email);
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
