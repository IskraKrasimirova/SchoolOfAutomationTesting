using OpenQA.Selenium;

namespace SeleniumTestFramework.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        // Elements 
        private IWebElement _emailInput => _driver.FindElement(By.XPath("//input[@type='email']"));
        private IWebElement _passwordInput => _driver.FindElement(By.XPath("//input[@type='password']"));
        private IWebElement _submitButton => _driver.FindElement(By.XPath("//button[@type='submit' and contains(text(), 'Sign In')]"));

        public LoginPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        // Actions
        public void LoginWith(string email, string password)
        {
            _emailInput.Clear();
            _emailInput.SendKeys(email);

            _passwordInput.Clear();
            _passwordInput.SendKeys(password);

            _submitButton.Click();
        }

        public string GetValidationMessage() => _driver.FindElement(By.ClassName("alert")).Text;

        public string? GetEmailBrowserValidationMessage() => _emailInput.GetAttribute("validationMessage");

        public string GetPasswordValidationMessage() => _driver.FindElement(By.ClassName("text-danger")).Text;

        public string? GetPasswordBrowserValidationMessage() => _passwordInput.GetAttribute("validationMessage");

        // Validations
        public void VerifyPasswordInputIsEmpty()
        {
            string? text = _passwordInput.GetAttribute("value");

            Assert.That(text, Is.EqualTo(string.Empty));
        }

        public bool IsPasswordInputEmpty()
        {
            return string.IsNullOrWhiteSpace(_passwordInput.GetAttribute("value"));
        }
    }
}
