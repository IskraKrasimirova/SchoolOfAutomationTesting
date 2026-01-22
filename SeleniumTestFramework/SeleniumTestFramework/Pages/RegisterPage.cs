using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTestFramework.Models;

namespace SeleniumTestFramework.Pages
{
    public class RegisterPage
    {
        private readonly IWebDriver _driver;

        // Elements 
        private IWebElement _titleDropdown => _driver.FindElement(By.XPath("//select[@id='title']"));
        private IWebElement _firstNameInput => _driver.FindElement(By.XPath("//input[@id='first_name']"));
        private IWebElement _surnameInput => _driver.FindElement(By.XPath("//input[@id='sir_name']"));
        private IWebElement _emailInput => _driver.FindElement(By.XPath("//input[@type='email']"));
        private IWebElement _passwordInput => _driver.FindElement(By.XPath("//input[@type='password']"));
        private IWebElement _countryInput => _driver.FindElement(By.XPath("//input[@id='country']"));
        private IWebElement _cityInput => _driver.FindElement(By.XPath("//input[@id='city']"));
        private IWebElement _agreementCheckbox => _driver.FindElement(By.XPath("//input[@type='checkbox' and @id='tos']"));
        private IWebElement _submitButton => _driver.FindElement(By.XPath("//button[@type='submit' and @name='signup']"));

        public RegisterPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void RegisterNewUser(RegisterModel model)
        {
            var select = new SelectElement(_titleDropdown); 
            select.SelectByText(model.Title);

            _firstNameInput.Clear();
            _firstNameInput.SendKeys(model.FirstName);

            _surnameInput.Clear();
            _surnameInput.SendKeys(model.Surname);

            _emailInput.Clear();
            _emailInput.SendKeys(model.Email);

            _passwordInput.Clear();
            _passwordInput.SendKeys(model.Password);

            _countryInput.Clear();
            _countryInput.SendKeys(model.Country);

            _cityInput.Clear();
            _cityInput.SendKeys(model.City);

            if (model.AgreeToTerms && !_agreementCheckbox.Selected)
            {
                _agreementCheckbox.Click();
            }

            _submitButton.Click();
        }
    }
}