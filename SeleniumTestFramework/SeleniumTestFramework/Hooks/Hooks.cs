using OpenQA.Selenium;
using Reqnroll;
using SeleniumTestFramework.Models;

namespace SeleniumTestFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        private static IWebDriver _driver; 
        private readonly SettingsModel _settingsModel;

        public Hooks(IWebDriver driver, SettingsModel settingsModel)
        {
            _driver = driver;
            this._settingsModel = settingsModel;
        }

        [AfterScenario] 
        public void AfterScenario() 
        { 
            _driver.Manage().Cookies.DeleteAllCookies(); 
            _driver.Navigate().GoToUrl(_settingsModel.BaseUrl); 
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
