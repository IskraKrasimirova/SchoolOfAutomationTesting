using Reqnroll;
using SeleniumTestFramework.ApiTests.Apis;
using SeleniumTestFramework.ApiTests.Models.Dtos;
using SeleniumTestFramework.ApiTests.Utils;
using StringUtils = SeleniumTestFramework.ApiTests.Utils.Types.StringUtils;


namespace SeleniumTestFramework.ApiTests.Steps
{
    [Binding]
    public class UsersApiSteps
    {
        private readonly UsersApi _usersApi;
        private readonly ScenarioContext _scenarioContext;

        public UsersApiSteps(UsersApi usersApi, ScenarioContext scenarioContext)
        {
            _usersApi = usersApi;
            _scenarioContext = scenarioContext;
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

        [Then("users response should contain the following data:")]
        public void ThenUsersResponseShouldContainTheFollowingData(DataTable dataTable)
        {
            var expectedUser = dataTable.CreateInstance<UserDto>();
            expectedUser.Password = StringUtils.Sha256(expectedUser.Password);

            var usersResponse = _scenarioContext.Get<UserDto>(ContextConstants.UsersResponse);

            Assert.That(usersResponse.Id, Is.EqualTo(expectedUser.Id));
            Assert.That(usersResponse.FirstName, Is.EqualTo(expectedUser.FirstName));
        }
    }
}
