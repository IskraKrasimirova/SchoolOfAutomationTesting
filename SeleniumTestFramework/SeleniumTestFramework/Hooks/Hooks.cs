using OpenQA.Selenium;
using Reqnroll;
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

        [AfterScenario("DeleteRegisteredUser", Order = 2)]
        public void DeleteCurrentUser()
        {
            var registeredUser = this._scenarioContext.Get<UserModel>(ContextConstants.RegisteredUser);
            _userOperations.DeleteUserWithEmail(registeredUser.Email);
        }

        [AfterScenario(Order = 9999)]
        public void Cleanup()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Exception cleanup: {ex.Message}");
            }
        }
    }
}
