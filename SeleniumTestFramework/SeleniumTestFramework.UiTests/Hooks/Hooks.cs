using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.DatabaseOperations.Entities;
using SeleniumTestFramework.DatabaseOperations.Operations;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Models.UserModels;
using SeleniumTestFramework.Utilities.Constants;

namespace SeleniumTestFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly UserOperations _userOperations;
        private readonly LocationOperations _locationOperations;

        public Hooks(ScenarioContext scenarioContext, IWebDriver driver, UserOperations userOperations, LocationOperations locationOperations)
        {
            this._scenarioContext = scenarioContext;
            this._driver = driver;
            this._userOperations = userOperations;
            this._locationOperations = locationOperations;
        }

        [AfterScenario(Order = 1)]
        public void CloseBrowser()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [AfterScenario(Order = 2)]
        public void Cleanup()
        {
            if (_scenarioContext.TryGetValue(ContextConstants.RegisteredUser, out RegisterModel registeredUser))
            {
                _userOperations.DeleteUserWithEmail(registeredUser.Email);
            }

            if (_scenarioContext.TryGetValue(ContextConstants.AddedUser, out AddUserModel addedUser))
            {
                _userOperations.DeleteUserWithEmail(addedUser.Email);
            }

            if (_scenarioContext.TryGetValue(ContextConstants.InsertedUser, out UserEntity insertedUser))
            {
                _userOperations.DeleteUserWithEmail(insertedUser.Email);
            }

            if (_scenarioContext.TryGetValue(ContextConstants.NewRegisteredUser, out UserModel user))
            {
                _userOperations.DeleteUserWithEmail(user.Email);
            }
        }

        [AfterScenario(Order = 3)]
        public void CleanupInsertedCity()
        {
            if (_scenarioContext.TryGetValue(ContextConstants.InsertedCity, out string cityName) &&
                _scenarioContext.TryGetValue(ContextConstants.InsertedCountry, out string countryName))
            {
                _locationOperations.DeleteCity(cityName, countryName);
            }
        }

        [AfterScenario("DeleteRegisteredUser", Order = 9999)]
        public void DeleteCurrentUser()
        {
            var registeredUser = this._scenarioContext.Get<UserModel>(ContextConstants.NewRegisteredUser);
            _userOperations.DeleteUserWithEmail(registeredUser.Email);
        }
    }
}
