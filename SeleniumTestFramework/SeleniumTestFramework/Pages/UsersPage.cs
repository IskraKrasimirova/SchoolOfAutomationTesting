using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace SeleniumTestFramework.Pages
{
    public class UsersPage
    {
        private readonly IWebDriver _driver;

        private IWebElement AvailableUsersHeader => _driver.FindElement(By.XPath("//h2[contains(., 'Available Users')]"));
        private IWebElement AddUserButton => _driver.FindElement(By.XPath("//button[@type='button' and contains(., 'Add User')]"));
        private IWebElement UsersTable => _driver.FindElement(By.XPath("//table[@id='users_list']"));

        public UsersPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public bool IsAtUsersPage()
            => _driver.Url.Contains("/users")
            && AvailableUsersHeader.Displayed
            && AddUserButton.Displayed;

        public IWebElement? FindUserRowByEmail(string email)
        {
            var rows = UsersTable.FindElements(By.XPath(".//tbody/tr"));

            foreach (var row in rows)
            {
                var emailCell = row.FindElements(By.XPath(".//td"))
                                   .FirstOrDefault(cell => cell.Text.Trim().Equals(email, StringComparison.OrdinalIgnoreCase));

                if (emailCell != null)
                    return row;
            }

            return null;
        }

        public void DeleteUser(string email)
        {
            var row = FindUserRowByEmail(email);
            if (row == null) 
                throw new InvalidOperationException($"User with email {email} was not found.");

            var deleteLink = row.FindElement(By.XPath(".//a[contains(@class, 'text-danger') and contains(., 'Delete')]"));
            new Actions(_driver).MoveToElement(deleteLink).Perform();
            deleteLink.Click();

            _driver.SwitchTo().Alert().Accept();
        }

    }
}
