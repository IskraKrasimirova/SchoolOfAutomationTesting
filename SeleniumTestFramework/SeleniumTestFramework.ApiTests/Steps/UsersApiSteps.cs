using FluentAssertions;
using FluentAssertions.Execution;
using Reqnroll;
using SeleniumTestFramework.ApiTests.Apis;
using SeleniumTestFramework.ApiTests.Models.Dtos;
using SeleniumTestFramework.ApiTests.Models.Factories;
using SeleniumTestFramework.ApiTests.Utils;
using StringUtils = SeleniumTestFramework.ApiTests.Utils.Types.StringUtils;


namespace SeleniumTestFramework.ApiTests.Steps
{
    [Binding]
    public class UsersApiSteps
    {
        private readonly UsersApi _usersApi;
        private readonly ScenarioContext _scenarioContext;
        private readonly IUserFactory _userFactory;

        public UsersApiSteps(ScenarioContext scenarioContext, UsersApi usersApi, IUserFactory userFactory)
        {
            _scenarioContext = scenarioContext;
            _usersApi = usersApi;
            _userFactory = userFactory;
        }

        [Given("I make a get request to users endpoint with id {int}")]
        public void GivenIMakeAGetRequestToUsersEndpointWithId(int id)
        {
            var response = _usersApi.GetUserById(id);
            var responseStatusCode = (int)response.StatusCode;

            _scenarioContext.Add(ContextConstants.StatusCode, responseStatusCode);

            if (response.IsSuccessful)
            {
                var responseBody = response.Data;
                _scenarioContext.Add(ContextConstants.UsersResponse, responseBody);
            }

            _scenarioContext.Add(ContextConstants.RawResponse, response.Content);
        }

        [Given("I make a post request to users endpoint with the following data:")]
        public void GivenIMakeAPostRequestToUsersEndpointWithTheFollowingData(DataTable dataTable)
        {
            var expectedUser = dataTable.CreateInstance<UserDto>();
            var timespan = DateTime.Now.ToFileTime();
            expectedUser.Email = expectedUser.Email.Replace("@", $"{timespan}@");

            var createUserResponse = _usersApi.CreateUser(expectedUser);

            var responseStatusCode = (int)createUserResponse.StatusCode;
            _scenarioContext.Add(ContextConstants.StatusCode, responseStatusCode);

            var responseBody = createUserResponse.Data;
            _scenarioContext.Add(ContextConstants.UsersResponse, responseBody);
        }

        [Given("I create a new user via the API")]
        public void GivenICreateANewUserViaTheAPI()
        {
            var newUser = _userFactory.CreateDefault();
            var createUserResponse = _usersApi.CreateUser(newUser);

            using (new AssertionScope())
            {
                createUserResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK); createUserResponse.Data.Should().NotBeNull();
                createUserResponse.Data.Id.Should().BeGreaterThan(0);
            }

            _scenarioContext.Add(ContextConstants.CreatedUserId, createUserResponse.Data.Id);
            // For Update only
            _scenarioContext.Add(ContextConstants.CreatedUserData, createUserResponse.Data);
        }

        [When("I delete that user")]
        public void WhenIDeleteThatUser()
        {
            var id = _scenarioContext.Get<int>(ContextConstants.CreatedUserId);
            var deleteResponse = _usersApi.DeleteUserById(id);

            _scenarioContext[ContextConstants.StatusCode] = (int)deleteResponse.StatusCode;
            _scenarioContext[ContextConstants.RawResponse] = deleteResponse.Content;
        }

        [When("I make a Delete request to users endpoint with id {int}")]
        public void WhenIMakeADeleteRequestToUsersEndpointWithId(int id)
        {
            var deleteResponse = _usersApi.DeleteUserById(id);

            _scenarioContext.Add(ContextConstants.StatusCode, (int)deleteResponse.StatusCode);
            _scenarioContext.Add(ContextConstants.RawResponse, deleteResponse.Content);
        }

        [When("I update that user with valid data")]
        public void WhenIUpdateThatUserWithValidData()
        {
            var id = _scenarioContext.Get<int>(ContextConstants.CreatedUserId);
            var originalUser = _scenarioContext.Get<UserDto>(ContextConstants.CreatedUserData);

            var updatedData = _userFactory.CreateCustom(
                title: "Mrs.",
                firstName: "AnaMaria",
                surname: originalUser.SirName,
                country: "Italy",
                city: "Rome",
                email: originalUser.Email
                );

            var updateResponse = _usersApi.UpdateUser(id, updatedData);

            _scenarioContext[ContextConstants.StatusCode] = (int)updateResponse.StatusCode;
            _scenarioContext[ContextConstants.UsersResponse] = updateResponse.Data;
            _scenarioContext[ContextConstants.UpdatedUserData] = updatedData;
        }

        [Then("users response should contain the following data:")]
        public void ThenUsersResponseShouldContainTheFollowingData(DataTable dataTable)
        {
            var expectedUser = dataTable.CreateInstance<UserDto>();
            expectedUser.Password = StringUtils.Sha256(expectedUser.Password);

            var usersResponse = _scenarioContext.Get<UserDto>(ContextConstants.UsersResponse);

            Assert.That(usersResponse.Id, Is.EqualTo(expectedUser.Id));
            Assert.That(usersResponse.FirstName, Is.EqualTo(expectedUser.FirstName));
        }

        [Then("create users response should contain the following data:")]
        public void ThenCreateUsersResponseShouldContainTheFollowingData(DataTable dataTable)
        {
            var expectedUser = dataTable.CreateInstance<UserDto>();
            var actualUser = _scenarioContext.Get<UserDto>(ContextConstants.UsersResponse);

            actualUser.Should().BeEquivalentTo(
            expectedUser,
            options => options
                .Excluding(u => u.Id)
                .Excluding(u => u.Password)
                .Excluding(u => u.Email)
            );
        }

        [Then("I make a get request to users endpoint with that id")]
        public void ThenIMakeAGetRequestToUsersEndpointWithThatId()
        {
            var id = _scenarioContext.Get<int>(ContextConstants.CreatedUserId);
            var getResponse = _usersApi.GetUserById(id);

            _scenarioContext[ContextConstants.StatusCode] = (int)getResponse.StatusCode;
            _scenarioContext[ContextConstants.RawResponse] = getResponse.Content;

            if (getResponse.IsSuccessful && getResponse.Data is not null)
            {
                _scenarioContext[ContextConstants.UsersResponse] = getResponse.Data;
            }
        }

        [Then("the updated user should have the new data")]
        public void ThenTheUpdatedUserShouldHaveTheNewData()
        {
            var id = _scenarioContext.Get<int>(ContextConstants.CreatedUserId);
            var actualUser = _scenarioContext.Get<UserDto>(ContextConstants.UsersResponse);

            actualUser.Id.Should().Be(id);

            var expectedUser = _scenarioContext.Get<UserDto>(ContextConstants.UpdatedUserData);

            //using (new AssertionScope())
            //{
            //    actualUser.Title.Should().Be(expectedUser.Title);
            //    actualUser.FirstName.Should().Be(expectedUser.FirstName);
            //    actualUser.SirName.Should().Be(expectedUser.SirName);
            //    actualUser.Country.Should().Be(expectedUser.Country);
            //    actualUser.City.Should().Be(expectedUser.City);
            //    actualUser.Email.Should().Be(expectedUser.Email);
            //}

            actualUser.Should().BeEquivalentTo(expectedUser,
                options => options
                .Excluding(u => u.Id)
                .Excluding(u => u.Password)
                );
        }
    }
}
