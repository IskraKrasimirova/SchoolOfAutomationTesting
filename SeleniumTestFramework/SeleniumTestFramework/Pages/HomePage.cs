using OpenQA.Selenium;

namespace SeleniumTestFramework.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private IWebElement EmailDropdown => _driver.FindElement(By.Id("navbarDropdown"));
        private IWebElement HomeLink => _driver.FindElement(By.LinkText("Home"));
        private IWebElement UsersLink => _driver.FindElement(By.LinkText("Users"));
        private IWebElement SearchLink => _driver.FindElement(By.LinkText("Search"));

        public HomePage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public string GetEmailElementText() => EmailDropdown.Text;

        public bool IsHomeLinkDisplayed() => HomeLink.Displayed;

        public bool IsUsersLinkDisplayed() => UsersLink.Displayed;

        public bool IsSearchLinkDisplayed() => SearchLink.Displayed;

        public bool IsAddUserLinkDisplayed()
        {
            var elements = _driver.FindElements(By.LinkText("Add User"));
            return elements.Count > 0 && elements[0].Displayed;
        }

        public string GetGreetingText() => _driver.FindElement(By.XPath("//h1[contains(@class, 'display-5')]")).Text;
    }
}
