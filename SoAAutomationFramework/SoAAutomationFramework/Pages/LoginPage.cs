using OpenQA.Selenium;
using SoAAutomationFramework.Actions;
using SoAAutomationFramework.Extensions;
using SoAAutomationFramework.Models;

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

        public void Login(LoginModel model)
        {
            EmailField.EnterText(model.Email);
            PasswordField.EnterText(model.Password);
            LoginButton.Click();
        }
        
        public string GetValidationMessage() => _driver.FindElement(By.ClassName("alert")).Text;

        public string? GetEmailBrowserValidationMessage() => EmailField.GetAttribute("validationMessage");

        public string GetPasswordValidationMessage() => _driver.FindElement(By.ClassName("text-danger")).Text;

        public string? GetPasswordBrowserValidationMessage() => PasswordField.GetAttribute("validationMessage");

        // Validations
        public bool IsPasswordInputEmpty()
        {
            return string.IsNullOrWhiteSpace(PasswordField.GetAttribute("value"));
        }
    }
}
