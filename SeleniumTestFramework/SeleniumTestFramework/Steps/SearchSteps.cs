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
        private readonly ScenarioContext _scenarioContext;
        private readonly UserOperations _userOperations;  
        private readonly SkillOperations _skillOperations;
        private readonly SearchPage _searchPage; 

        public SearchSteps(ScenarioContext scenarioContext, UserOperations userOperations, SkillOperations skillOperations, SearchPage searchPage)
        {
            this._scenarioContext = scenarioContext;
            this._userOperations = userOperations;
            this._skillOperations = skillOperations;
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

        [When("I search for users with skill {string}")]
        public void WhenISearchForUsersWithSkill(string skillName)
        {
            _searchPage.VerifyIsAtSearchPage();
            _searchPage.SelectSkill(skillName);
            _searchPage.ClickSearch();
        }
    }
}
