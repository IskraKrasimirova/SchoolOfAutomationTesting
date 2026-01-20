using OpenQA.Selenium;
using SoAAutomationFramework.Actions;

namespace SoAAutomationFramework.Pages
{
    public class LoginPage : BaseUserActions
    {
        private IWebElement EmailField => Driver.FindElement(By.XPath("//input[@type='email']"));
        private IWebElement PasswordField => Driver.FindElement(By.XPath("//input[@type='password']"));
        private IWebElement LoginButton => Driver.FindElement(By.XPath("//button[@type='submit']"));

        public LoginPage() : base()
        {

        }

        public void Login(string email, string password)
        {
            EmailField.Clear();
            EmailField.SendKeys(email);
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }
    }
}
