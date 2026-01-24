using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTestFramework.Extensions;
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
        public IWebElement AlertElement => _driver.FindElement(By.XPath("//form//div[contains(@class,'alert-warning')]"));

        public RegisterPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void RegisterNewUser(RegisterModel model)
        {
            var select = new SelectElement(TitleDropdown);
            select.SelectByText(model.Title);

            FirstNameInput.EnterText(model.FirstName);
            SurnameInput.EnterText(model.Surname);
            EmailInput.EnterText(model.Email);
            PasswordInput.EnterText(model.Password);
            CountryInput.EnterText(model.Country);
            CityInput.EnterText(model.City);

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

        public string GetGlobalAlertMessage() => AlertElement.Text.Trim();

        public bool IsPasswordInputEmpty()
        {
            return string.IsNullOrWhiteSpace(PasswordInput.GetAttribute("value"));
        }

        private string GetValidationMessage(IWebElement element)
        {
            var messageElement = element.FindElement(By.XPath("./following-sibling::div[@class='invalid-feedback']"));

            return messageElement.Text;
        }
    }
}