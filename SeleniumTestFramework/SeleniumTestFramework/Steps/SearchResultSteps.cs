using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.DatabaseOperations.Entities;
using SeleniumTestFramework.DatabaseOperations.Operations;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
