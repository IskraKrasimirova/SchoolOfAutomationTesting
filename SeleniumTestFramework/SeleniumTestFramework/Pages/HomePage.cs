using OpenQA.Selenium;

namespace SeleniumTestFramework.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private IWebElement _emailDropdown => _driver.FindElement(By.Id("navbarDropdown"));
        private IWebElement _homeLink => _driver.FindElement(By.LinkText("Home"));
        private IWebElement _usersLink => _driver.FindElement(By.LinkText("Users"));
        private IWebElement _searchLink => _driver.FindElement(By.LinkText("Search"));

        public HomePage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public string GetEmailElementText() => _emailDropdown.Text;

        public bool IsHomeLinkDisplayed() => _homeLink.Displayed;

        public bool IsUsersLinkDisplayed() => _usersLink.Displayed;

        public bool IsSearchLinkDisplayed() => _searchLink.Displayed;

        public bool IsAddUserLinkDisplayed()
        {
            var elements = _driver.FindElements(By.LinkText("Add User"));
            return elements.Count > 0 && elements[0].Displayed;
        }
    }
}
