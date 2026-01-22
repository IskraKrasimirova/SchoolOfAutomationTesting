using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTestFramework.Models;

namespace SeleniumTestFramework.Pages
{
    public class RegisterPage
    {
        private readonly IWebDriver _driver;

        private IWebElement TitleDropdown => _driver.FindElement(By.XPath("//select[@id='title']"));
        private IWebElement FirstNameInput => _driver.FindElement(By.XPath("//input[@id='first_name']"));
        private IWebElement SurnameInput => _driver.FindElement(By.XPath("//input[@id='sir_name']"));
        private IWebElement EmailInput => _driver.FindElement(By.XPath("//input[@type='email']"));
        private IWebElement PasswordInput => _driver.FindElement(By.XPath("//input[@type='password']"));
        private IWebElement CountryInput => _driver.FindElement(By.XPath("//input[@id='country']"));
        private IWebElement CityInput => _driver.FindElement(By.XPath("//input[@id='city']"));
        private IWebElement AgreementCheckbox => _driver.FindElement(By.XPath("//input[@type='checkbox' and @id='tos']"));
        private IWebElement SubmitButton => _driver.FindElement(By.XPath("//button[@type='submit' and @name='signup']"));

        public RegisterPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void RegisterNewUser(RegisterModel model)
        {
            var select = new SelectElement(TitleDropdown);
            select.SelectByText(model.Title);

            FirstNameInput.Clear();
            FirstNameInput.SendKeys(model.FirstName);

            SurnameInput.Clear();
            SurnameInput.SendKeys(model.Surname);

            EmailInput.Clear();
            EmailInput.SendKeys(model.Email);

            PasswordInput.Clear();
            PasswordInput.SendKeys(model.Password);

            CountryInput.Clear();
            CountryInput.SendKeys(model.Country);

            CityInput.Clear();
            CityInput.SendKeys(model.City);

            if (model.AgreeToTerms && !AgreementCheckbox.Selected)
            {
                AgreementCheckbox.Click();
            }

            SubmitButton.Click();
        }

        public string GetFirstNameValidationMessage() => GetValidationMessage(FirstNameInput);

        public string GetSurnameValidationMessage() => GetValidationMessage(SurnameInput);

        public string GetEmailValidationMessage() => GetValidationMessage(EmailInput);

        public string GetPasswordValidationMessage() => GetValidationMessage(PasswordInput);

        public string GetCountryValidationMessage() => GetValidationMessage(CountryInput);

        public string GetCityValidationMessage() => GetValidationMessage(CityInput);

        public string GetAgreementValidationMessage() => GetValidationMessage(AgreementCheckbox);

        private string GetValidationMessage(IWebElement element)
        {
            var messageElement = element.FindElement(By.XPath("./following-sibling::div[@class='invalid-feedback']"));

            return messageElement.Text;
        }
    }
}