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
        }

        [When("I delete that user")]
        public void WhenIDeleteThatUser()
        {
            var id = _scenarioContext.Get<int>(ContextConstants.CreatedUserId);
            var deleteResponse = _usersApi.DeleteUserById(id);

            _scenarioContext[ContextConstants.StatusCode] = (int)deleteResponse.StatusCode;
            _scenarioContext[ContextConstants.RawResponse] = deleteResponse.Content;
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
                _scenarioContext[ContextConstants.UsersResponse]= getResponse.Data;
            }
        }
    }
}
