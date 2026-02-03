using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.DatabaseOperations.Entities;
using SeleniumTestFramework.DatabaseOperations.Operations;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities.Constants;

namespace SeleniumTestFramework.Steps
{
    [Binding]
    public class SearchSteps
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly UserOperations _userOperations;  
        private readonly SkillOperations _skillOperations;
        private readonly LocationOperations _locationOperations;
        private readonly SearchPage _searchPage; 

        public SearchSteps(IWebDriver driver, ScenarioContext scenarioContext, UserOperations userOperations, SkillOperations skillOperations, LocationOperations locationOperations, SearchPage searchPage)
        {
            this._driver = driver;
            this._scenarioContext = scenarioContext;
            this._userOperations = userOperations;
            this._skillOperations = skillOperations;
            this._locationOperations = locationOperations;
            this._searchPage = searchPage;
        }

        [Given("a user exists in the database with:")]
        public void GivenAUserExistsInTheDatabaseWith(DataTable table)
        {
            var row = table.Rows[0]; 

            var user = new UserEntity 
            { 
                FirstName = row["firstName"], 
                Surname = row["surname"], 
                Email = row["email"], 
                Country = row["country"], 
                City = row["city"], 
                Title = row["title"], 
                Password = row["password"], 
                IsAdmin = bool.Parse(row["isAdmin"]) }; 

            _userOperations.DeleteUserWithEmail(user.Email); 

            var userId = _userOperations.InsertUser(user);
            user.Id = userId;

            _scenarioContext.Add(ContextConstants.InsertedUser, user);
        }

        [Given("the user has the following skills:")]
        public void GivenTheUserHasTheFollowingSkills(DataTable dataTable)
        {
            var user = _scenarioContext.Get<UserEntity>(ContextConstants.InsertedUser);

            foreach (var row in dataTable.Rows)
            {
                var skillName = row["skillName"];
                var skillCompetene = int.Parse(row["competence"]);

                var skillId = _skillOperations.GetSkillIdByName(skillName);

                _skillOperations.AddSkillToUser(user.Id, skillId, skillCompetene);
            }
        }

        [Given("a country exists in the database with name {string}")]
        public void GivenACountryExistsInTheDatabaseWithName(string countryName)
        {
            _locationOperations.DeleteCountry(countryName);
            _locationOperations.InsertCityAndCountry("TEMP_CITY", countryName);
            _scenarioContext.Add(ContextConstants.InsertedCountry, countryName);
        }

        [Given("a city exists in the database with name {string} in country {string}")]
        public void GivenACityExistsInTheDatabaseWithNameInCountry(string cityName, string countryName)
        {
            _locationOperations.DeleteCity(cityName, countryName);
            _locationOperations.InsertCityAndCountry(cityName, countryName);
        }

        [When("I search for users with skill {string}")]
        public void WhenISearchForUsersWithSkill(string skillName)
        {
            _searchPage.VerifyIsAtSearchPage();
            _searchPage.SelectSkill(skillName);
            _searchPage.ClickSearch();
        }

        [When("I refresh the search page")]
        public void WhenIRefreshTheSearchPage()
        {
            _driver.Navigate().Refresh();
        }

        [When("I open the country dropdown")]
        public void WhenIOpenTheCountryDropdown()
        {
            _searchPage.VerifyIsAtSearchPage();
            _searchPage.OpenCountryDropdown();
        }

        [When("I select country {string}")]
        public void WhenISelectCountry(string countryName)
        {
            _searchPage.VerifyCountryExists(countryName);
            _searchPage.SelectCountry(countryName);
        }

        [Then("I should see {string} in the country dropdown")]
        public void ThenIShouldSeeInTheCountryDropdown(string countryName)
        {
            _searchPage.VerifyCountryExists(countryName);
        }

        [Then("I should see {string} in the city dropdown")]
        public void ThenIShouldSeeInTheCityDropdown(string cityName)
        {
            _searchPage.VerifyCityExists(cityName);
        }
    }
}
