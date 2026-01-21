using OpenQA.Selenium;
using SoAAutomationFramework.Actions;

namespace SoAAutomationFramework.Pages
{
    public class HomePage : BaseUserActions
    {
        private IWebElement EmailDropdown => Driver.FindElement(By.Id("navbarDropdown"));
        private IWebElement HomeLink => Driver.FindElement(By.LinkText("Home"));
        private IWebElement UsersLink => Driver.FindElement(By.LinkText("Users"));
        private IWebElement SearchLink => Driver.FindElement(By.LinkText("Search"));

        public HomePage() : base()
        {
        }

        public string GetEmailElementText() => EmailDropdown.Text;

        public bool IsHomeLinkDisplayed() => HomeLink.Displayed;

        public bool IsUsersLinkDisplayed() => UsersLink.Displayed;

        public bool IsSearchLinkDisplayed() => SearchLink.Displayed;

        public bool IsAddUserLinkDisplayed()
        {
            var elements = Driver.FindElements(By.LinkText("Add User"));
            return elements.Count > 0 && elements[0].Displayed;
        }
    }
}
