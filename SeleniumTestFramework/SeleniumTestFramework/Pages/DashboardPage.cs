using OpenQA.Selenium;
using SeleniumTestFramework.Extensions;
using SeleniumTestFramework.Utilities;

namespace SeleniumTestFramework.Pages
{
    public class DashboardPage
    {
        private readonly IWebDriver _driver;

        private IWebElement LoggedUserAnchor => _driver.FindElement(By.XPath("//a[@id='navbarDropdown']"));
        private IWebElement UsernameHeader => _driver.FindElement(By.XPath("//div[contains(@class, 'container-fluid')]/h1"));
        private IWebElement HomeLink => _driver.FindElement(By.XPath("//a[contains(text(), 'Home')]"));
        private IWebElement UsersLink => _driver.FindElement(By.XPath("//a[contains(text(), 'Users')]"));
        private IWebElement SearchLink => _driver.FindElement(By.XPath("//a[contains(text(), 'Search')]"));
        private IWebElement LogoutLink => _driver.FindElement(By.XPath("//a[@class='dropdown-item' and contains(., 'Logout')]"));
        private By LogoutLinkLocator => By.XPath("//a[@class='dropdown-item' and contains(., 'Logout')]");

        public DashboardPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public string GetLoggedUserEmail()
        {
            Retry.Until(() =>
            {
                if (!LoggedUserAnchor.Displayed)
                    throw new RetryException("Logged user anchor not visible yet.");
            });

            return LoggedUserAnchor.Text.Trim();
        }

        public bool IsHomeLinkDisplayed() => HomeLink.Displayed;

        public bool IsUsersLinkDisplayed() => UsersLink.Displayed;

        public bool IsSearchLinkDisplayed() => SearchLink.Displayed;

        public bool IsAddUserLinkDisplayed()
        {
            var elements = _driver.FindElements(By.LinkText("Add User"));
            return elements.Count > 0 && elements[0].Displayed;
        }

        public string GetGreetingText() => UsernameHeader.Text.Trim();

        public void Logout()
        {
            Retry.Until(() =>
            {
                if (!LoggedUserAnchor.Displayed)
                    throw new RetryException("User dropdown not visible yet.");

                LoggedUserAnchor.Click();
            });

            // Error in console 'bootstrap is not defined'
            // Enforce opening the dropdown
            ((IJavaScriptExecutor)_driver)
                .ExecuteScript("document.querySelector('#navbarDropdown').setAttribute('aria-expanded', 'true');" +
                               "document.querySelector('.dropdown-menu').style.display = 'block';");

            Retry.Until(() =>
            {
                if (!LogoutLink.Displayed)
                    throw new RetryException("Logout link not visible yet.");
            });

            LogoutLink.Click();
        }

        public UsersPage GoToUsersPage()
        {
            UsersLink.Click();

            return new UsersPage(_driver);
        }

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

        public void VerifyIsAtDashboardPage()
        {
            Retry.Until(() =>
            {
                if (!LoggedUserAnchor.Displayed)
                    throw new RetryException("Dashboard not fully loaded yet.");
            });

            Assert.Multiple(() =>
            {
                Assert.That(_driver.Url, Does.Contain("/index.php"), "Did not navigate to Dashboard page.");
                Assert.That(LoggedUserAnchor.Displayed, "Logged user link is not visible.");
                Assert.That(UsernameHeader.Displayed, "User greeting  header is not visible.");
                Assert.That(HomeLink.Displayed, "Home link is not visible.");
                Assert.That(UsersLink.Displayed, "Users link is not visible.");
                Assert.That(SearchLink.Displayed, "Search link is not visible.");
            });
        }

        public void VerifyUserIsLoggedIn(string email, string username, bool isAdmin)
        {
            Retry.Until(() =>
            {
                if (!LoggedUserAnchor.Displayed)
                    throw new RetryException("User dropdown not visible yet.");
            });

            var loggedUserEmail = GetLoggedUserEmail();
            Assert.That(loggedUserEmail, Is.EqualTo(email), "User email is not shown.");

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
    }
}
