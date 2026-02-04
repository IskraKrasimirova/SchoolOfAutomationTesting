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
            _searchResultPage.VerifyAllRowsHaveCountry(countryName);
        }

        [Then("all results should contain only cities:")]
        public void ThenAllResultsShouldContainOnlyCities(DataTable dataTable)
        {
            var expectedCities = dataTable.Rows.
                Select(r => r["City"].Trim())
                .ToList();

            _searchResultPage.VerifyRowsContainOnlyCities(expectedCities);
        }
    }
}
