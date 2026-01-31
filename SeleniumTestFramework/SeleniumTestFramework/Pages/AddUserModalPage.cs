using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTestFramework.Extensions;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestFramework.Pages
{
    public class AddUserModalPage
    {
        private readonly IWebDriver _driver;

        private IWebElement AddUserHeader => _driver.FindElement(By.XPath("//h5[@id='addUserModalLabel']"));
        private IWebElement TitleDropdown => _driver.FindElement(By.XPath("//select[@id='title']"));
        private IWebElement FirstNameInput => _driver.FindElement(By.XPath("//input[@id='first_name']"));
        private IWebElement SurnameInput => _driver.FindElement(By.XPath("//input[@id='sir_name']"));
        private IWebElement EmailInput => _driver.FindElement(By.XPath("//input[@type='email']"));
        private IWebElement PasswordInput => _driver.FindElement(By.XPath("//input[@type='password']"));
        private IWebElement CountryDropdown => _driver.FindElement(By.XPath("//select[@id='country']"));
        private IWebElement CityInput => _driver.FindElement(By.XPath("//input[@id='city']"));
        private IWebElement IsAdminCheckbox => _driver.FindElement(By.XPath("//input[@type='checkbox' and @id='is_admin']"));
        private IWebElement SubmitButton => _driver.FindElement(By.XPath("//button[@type='submit' and text()='Add User']"));

        public AddUserModalPage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void AddUser(AddUserModel model)
        {
            var titleSelect = new SelectElement(TitleDropdown);
            titleSelect.SelectByValue(model.Title);

            FirstNameInput.EnterText(model.FirstName);
            SurnameInput.EnterText(model.Surname);

            var countrySelect = new SelectElement(CountryDropdown);
            countrySelect.SelectByValue(model.Country);

            CityInput.EnterText(model.City);
            EmailInput.EnterText(model.Email);
            PasswordInput.EnterText(model.Password);

            if (model.IsAdmin && !IsAdminCheckbox.Selected)
            {
                IsAdminCheckbox.Click();
            }

            _driver.ScrollToElementAndClick(SubmitButton);
        }

        public void VerifyModalIsClosed()
        {
            Retry.Until(() =>
            {
                var modal = _driver.FindElement(By.XPath("//div[@id='addUserModal' and contains(@class,'show')]"));
                if (modal != null && modal.Displayed)
                    throw new RetryException("Add User modal is still visible.");
            });
        }

        public void VerifyIsAtAddUserModal()
        {
            Retry.Until(() =>
            {
                if (!AddUserHeader.Displayed)
                    throw new RetryException("Add User modal is not displayed yet.");
            });

            Assert.That(AddUserHeader.Text.Trim(), Is.EqualTo("Add New User"));
        }
    }
}
