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
            Assert.That(statusCode, Is.EqualTo(expectedStatusCode));
        }
    }
}
