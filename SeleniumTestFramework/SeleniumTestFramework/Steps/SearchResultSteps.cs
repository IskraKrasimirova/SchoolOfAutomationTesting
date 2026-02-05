using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.DatabaseOperations.Entities;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities.Constants;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class SearchResultSteps
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly SearchPage _searchPage;
        private readonly SearchResultPage _searchResultPage;

        public SearchResultSteps(IWebDriver driver, ScenarioContext scenarioContext, SearchPage searchPage, SearchResultPage searchResultPage)
        {
            this._driver = driver;
            this._scenarioContext = scenarioContext;
            this._searchPage = searchPage;
            this._searchResultPage = searchResultPage;
        }

        [Then("all results should contain skill {string}")]
        public void ThenAllResultsShouldContainSkill(string skillName)
        {
            _searchResultPage.VerifyIsAtSearchResultPage();
            _searchResultPage.VerifyResultsTableIsVisible();
            _searchResultPage.VerifyAllRowsHaveSkill(skillName);
        }

        [Then("I should see the created user in the search results")]
        public void ThenIShouldSeeTheCreatedUserInTheSearchResults()
        {
            var user = _scenarioContext.Get<UserEntity>(ContextConstants.InsertedUser);
            _searchResultPage.VerifyUserExists(user.Email);
        }

        [Then("all results should contain country {string}")]
        public void ThenAllResultsShouldContainCountry(string countryName)
        {
            _searchResultPage.VerifyIsAtSearchResultPage();
            _searchResultPage.VerifyResultsTableIsVisible();
            _searchResultPage.VerifyAllRowsHaveCountry(countryName);
        }

        [Then("all results should contain only countries:")]
        public void ThenAllResultsShouldContainOnlyCountries(DataTable dataTable)
        {
            var expectedCountries = dataTable.Rows.
                Select(r => r["Country"].Trim())
                .ToList();

            _searchResultPage.VerifyRowsContainOnlyCountries(expectedCountries);
        }


        [Then("all results should contain only cities:")]
        public void ThenAllResultsShouldContainOnlyCities(DataTable dataTable)
        {
            var expectedCities = dataTable.Rows.
                Select(r => r["City"].Trim())
                .ToList();

            _searchResultPage.VerifyRowsContainOnlyCities(expectedCities);
        }

        [Then("no users should be found")]
        public void ThenNoUsersShouldBeFound()
        {
            _searchResultPage.VerifyIsAtSearchResultPage();
            _searchResultPage.VerifyNoUsersFound();
        }

        [Then("I should see a message with the following text {string}")]
        public void ThenIShouldSeeAMessageWithTheFollowingText(string expectedMessage)
        {
            _searchResultPage.VerifyInfoMessage(expectedMessage);
        }

        [Then("the results should show every skill for every user")]
        public void ThenTheResultsShouldShowEverySkillForEveryUser()
        {
            var actualTableRowsCount = _searchResultPage.GetAllRowsInResultTable();
            var expectedRowsCount = _scenarioContext.Get<int>(ContextConstants.UserSkillsCount);

            Assert.That(actualTableRowsCount, Is.EqualTo(expectedRowsCount));
        }
    }
}
