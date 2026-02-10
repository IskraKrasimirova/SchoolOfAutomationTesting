using FluentAssertions;
using Reqnroll;
using SeleniumTestFramework.ApiTests.Utils;

namespace SeleniumTestFramework.ApiTests.Steps
{
    [Binding]
    public class CommonApiSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CommonApiSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then("the response status code should be {int}")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            var statusCode = _scenarioContext.Get<int>(ContextConstants.StatusCode); 

            statusCode.Should().Be(expectedStatusCode);
        }

        [Then("the response should contain the following error message {string}")]
        public void ThenTheResponseShouldContainTheFollowingErrorMessage(string errorMessage)
        {
            var response = _scenarioContext.Get<string>(ContextConstants.RawResponse);

            response.Should().Contain(errorMessage, $"Expected the response to contain error message '{errorMessage}'");
        }

        [Then("the response should contain the following message {string}")]
        public void ThenTheResponseShouldContainTheFollowingMessage(string expectedMessage)
        {
            var response = _scenarioContext.Get<string>(ContextConstants.RawResponse);

            response.Should().Contain(expectedMessage, $"Expected the response to contain error message '{expectedMessage}'");
        }
    }
}
