using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.DatabaseOperations.Operations;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Utilities.Constants;

namespace SeleniumTestFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IWebDriver _driver;
        private readonly UserOperations _userOperations;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext, IWebDriver driver, UserOperations userOperations)
        {
            this._scenarioContext = scenarioContext;
            this._driver = driver;
            this._userOperations = userOperations;
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
            if (_scenarioContext.TryGetValue(ContextConstants.RegisteredUser, out RegisterModel user))
            {
                _userOperations.DeleteUserWithEmail(user.Email);
            }

            if (_scenarioContext.TryGetValue(ContextConstants.AddedUser, out AddUserModel addedUser))
            {
                _userOperations.DeleteUserWithEmail(addedUser.Email);
            }
        }

        [AfterScenario("DeleteRegisteredUser", Order = 9999)]
        public void DeleteCurrentUser()
        {
            var registeredUser = this._scenarioContext.Get<RegisterModel>(ContextConstants.RegisteredUser);
            _userOperations.DeleteUserWithEmail(registeredUser.Email);
        }
    }
}
