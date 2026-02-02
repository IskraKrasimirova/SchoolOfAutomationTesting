using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumTestFramework.Extensions;
using SeleniumTestFramework.Utilities;

namespace SeleniumTestFramework.Pages
{
    public class UsersPage: BasePage
    {
        private readonly By AddUserButtonLocator = By.XPath("//button[@type='button' and contains(., 'Add User')]");
        private IWebElement AvailableUsersHeader => _driver.FindElement(By.XPath("//h2[contains(., 'Available Users')]"));
        private IWebElement AddUserButton => _driver.FindElement(AddUserButtonLocator);
        private IWebElement UsersTable => _driver.FindElement(By.XPath("//table[@id='users_list']"));

        // Dynamic elements 
        private IWebElement GetDeleteButtonForEmail(string email) => _driver.FindElement(By.XPath($"//td[contains(text(), '{email}')]/following-sibling::td/a"));

        private IWebElement? FindUserRowByEmail(string email) => _driver.FindElements(By.XPath($"//td[contains(text(), '{email}')]/parent::tr"))
           .FirstOrDefault();

        public UsersPage(IWebDriver driver): base(driver)
        {
        }

        public void DeleteUser(string email)
        {
            var deleteLink = GetDeleteButtonForEmail(email);
            Assert.That(deleteLink, Is.Not.Null, $"Delete button for {email} was not found.");

            new Actions(_driver).MoveToElement(deleteLink).Perform();
            deleteLink.Click();

            _driver.SwitchTo().Alert().Accept();
        }

        public AddUserModalPage OpenAddUserModal()
        {
            AddUserButton.Click();
            return new AddUserModalPage(_driver);
        }

        public bool IsAddUserButtonDisplayed()
        {
            var addUserButtons = _driver.FindElements(AddUserButtonLocator);
            return addUserButtons.Count > 0 && addUserButtons[0].Displayed;
        }

        public void VerifyIsAtUsersPage(bool isAdmin)
        {
            _driver.WaitUntilUrlContains("/users");

            Retry.Until(() =>
            {
                if (!AvailableUsersHeader.Displayed)
                    throw new RetryException("Users page not loaded yet.");
            });

            Assert.That(UsersTable.Displayed, "Users table is not visible.");

            var isAddUserButtonVisible = IsAddUserButtonDisplayed();

            if (isAdmin)
            {
                Assert.That(isAddUserButtonVisible, "Add User button should be visible for admin.");
            }
            else
            {
                Assert.That(isAddUserButtonVisible, Is.False, "Add User button should NOT be visible for common user.");
            }
        }

        public void VerifyUserExists(string email)
        {
            IWebElement? userRow = FindUserRowByEmail(email);
            Assert.That(userRow, Is.Not.Null, $"User with email {email} was not found.");
        }

        public void VerifyUserDoesNotExist(string email)
        {
            Retry.Until(() =>
            {
                var userRow = FindUserRowByEmail(email);
                if (userRow != null)
                    throw new RetryException($"User with email {email} is still present.");
            });
        }
    }
}
