using OpenQA.Selenium;
using SeleniumTestFramework.Extensions;

namespace SeleniumTestFramework.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        // Elements 
        private IWebElement EmailInput => _driver.FindElement(By.XPath("//input[@type='email']"));
        private IWebElement PasswordInput => _driver.FindElement(By.XPath("//input[@type='password']"));
        private IWebElement SubmitButton => _driver.FindElement(By.XPath("//button[@type='submit' and contains(text(), 'Sign In')]"));

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Actions
        public void LoginWith(string email, string password)
        {
            EmailInput.EnterText(email);
            PasswordInput.EnterText(password);

            SubmitButton.Click();
        }

        public bool IsAtLoginPage() => _driver.Url.Contains("/login");

        public string GetValidationMessage() => _driver.FindElement(By.ClassName("alert")).Text;

        public string? GetEmailBrowserValidationMessage() => EmailInput.GetAttribute("validationMessage");

        public string GetPasswordValidationMessage() => _driver.FindElement(By.ClassName("text-danger")).Text;

        public string? GetPasswordBrowserValidationMessage() => PasswordInput.GetAttribute("validationMessage");

        public bool IsPasswordInputEmpty()
        {
            return string.IsNullOrWhiteSpace(PasswordInput.GetAttribute("value"));
        }

        // Validations
        public void VerifyPasswordInputIsEmpty()
        {
            string? text = PasswordInput.GetAttribute("value");
            Assert.That(text, Is.EqualTo(string.Empty));
        }

        public void VerifyErrorMessageIsDisplayed(string errorMessage)
        {
            var errorDialogText = _driver.FindElement(By.ClassName("alert")).Text;
            Assert.That(errorDialogText, Is.EqualTo(errorMessage));
        }
    }
}
