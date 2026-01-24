using OpenQA.Selenium;
using SeleniumTestFramework.Extensions;

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
        private IWebElement LogoutLink => _driver.FindElement(By.XPath("//a[@class='dropdown-item' and contains(., 'Logout')]"));
        private By LogoutLinkLocator => By.XPath("//a[@class='dropdown-item' and contains(., 'Logout')]");

        public DashboardPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void Logout()
        {
            LoggedUserAnchor.Click();

            //var logoutLink = _driver.FindElements(LogoutLinkLocator).FirstOrDefault(); _driver.WaitUntilElementIsClickable(logoutLink);

            // Форсирано отваряне на dropdown-а
            // Error in console 'bootstrap is not defined'
            ((IJavaScriptExecutor)_driver)
            .ExecuteScript("document.querySelector('#navbarDropdown').setAttribute('aria-expanded', 'true');" + "document.querySelector('.dropdown-menu').style.display = 'block';");

            var logoutLink = _driver.FindElement(LogoutLinkLocator);
            logoutLink.Click();
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

        public void VerifyUserIsLoggedIn(string email, string username, bool isAdmin)
        {
            var emailDropdownText = GetLoggedUserEmail();
            Assert.That(emailDropdownText, Is.EqualTo(email), "User email is not shown.");

            var greetingText = GetGreetingText();
            Assert.That(greetingText, Does.Contain(username), "The greeting text does not contain the username of the logged-in user.");

            Assert.Multiple(() =>
            {
                Assert.That(IsHomeLinkDisplayed(), "Home link is not displayed.");
                Assert.That(IsUsersLinkDisplayed(), "Users link is not displayed.");
                Assert.That(IsSearchLinkDisplayed(), "Search link is not displayed.");
            });

            if (isAdmin)
            {
                Assert.That(IsAddUserLinkDisplayed(), "Add User link should be visible for admin.");
            }
            else
            {
                Assert.That(IsAddUserLinkDisplayed(), Is.False, "Add User link should NOT be visible for common user.");
            }
        }

        public UsersPage GoToUsersPage() 
        { 
            UsersLink.Click(); 
            return new UsersPage(_driver); 
        }
    }
}
