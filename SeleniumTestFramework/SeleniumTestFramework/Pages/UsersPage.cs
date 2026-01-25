using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumTestFramework.Extensions;
using SeleniumTestFramework.Utilities;

namespace SeleniumTestFramework.Pages
{
    public class UsersPage
    {
        private readonly IWebDriver _driver;

        private readonly By AddUserButtonLocator = By.XPath("//button[@type='button' and contains(., 'Add User')]");
        private IWebElement AvailableUsersHeader => _driver.FindElement(By.XPath("//h2[contains(., 'Available Users')]"));
        private IWebElement AddUserButton => _driver.FindElement(AddUserButtonLocator);
        private IWebElement UsersTable => _driver.FindElement(By.XPath("//table[@id='users_list']"));

        public UsersPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public IWebElement? FindUserRowByEmail(string email)
        {
            var userRows = UsersTable.FindElements(By.XPath(".//tbody/tr"));

            foreach (var userRow in userRows)
            {
                var emailCell = userRow.FindElements(By.XPath(".//td"))
                                   .FirstOrDefault(cell => cell.Text.Trim().Equals(email, StringComparison.OrdinalIgnoreCase));

                if (emailCell != null)
                    return userRow;
            }

            return null;
        }

        public void DeleteUser(string email)
        {
            var userRow = FindUserRowByEmail(email);
            Assert.That(userRow, Is.Not.Null, $"User with email {email} was not found.");

            var deleteLink = userRow.FindElement(By.XPath(".//a[contains(@class, 'text-danger') and contains(., 'Delete')]"));
            new Actions(_driver).MoveToElement(deleteLink).Perform();
            deleteLink.Click();

            _driver.SwitchTo().Alert().Accept();
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
