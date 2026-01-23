using OpenQA.Selenium;

namespace SeleniumTestFramework.Pages
{
    public class DashboardPage
    {
        private readonly IWebDriver _driver;

        private IWebElement LoggedUserAnchor => _driver.FindElement(By.XPath("//a[@id='navbarDropdown']"));
        private IWebElement UsernameHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]/h1"));
        private IWebElement HomeLink => _driver.FindElement(By.LinkText("Home"));
        private IWebElement UsersLink => _driver.FindElement(By.LinkText("Users"));
        private IWebElement SearchLink => _driver.FindElement(By.LinkText("Search"));

        public DashboardPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public string GetLoggedUserEmail() => LoggedUserAnchor.Text;

        public bool IsHomeLinkDisplayed() => HomeLink.Displayed;

        public bool IsUsersLinkDisplayed() => UsersLink.Displayed;

        public bool IsSearchLinkDisplayed() => SearchLink.Displayed;

        public bool IsAddUserLinkDisplayed()
        {
            var elements = _driver.FindElements(By.LinkText("Add User"));
            return elements.Count > 0 && elements[0].Displayed;
        }

        public string GetGreetingText() => UsernameHeader.Text.Trim();

        // Validations
        public void VerifyLoggedUserEmailIs(string expectedUserEmail)
        {
            string actualUserEmail = this.LoggedUserAnchor.Text.Trim();

            Assert.That(actualUserEmail, Is.EqualTo(expectedUserEmail));
        }

        public void VerifyUsernameIs(string username)
        {
            string headerText = this.UsernameHeader.Text.Trim();
            Assert.That(headerText.Contains(username), Is.True);
        }
    }
}
