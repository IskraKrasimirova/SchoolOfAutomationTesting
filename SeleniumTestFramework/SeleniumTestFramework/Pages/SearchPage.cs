using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTestFramework.Utilities;
using SeleniumTestFramework.Utilities.Extensions;

namespace SeleniumTestFramework.Pages
{
    public class SearchPage : BasePage
    {
        private IWebElement CountryHeader => _driver.FindElement(By.XPath("//h2[contains(text(),'Countries')]"));
        private IWebElement CityHeader => _driver.FindElement(By.XPath("//h2[contains(text(),'Cities')]"));
        private IWebElement SkillsHeader => _driver.FindElement(By.XPath("//h2[contains(text(),'Skills')]"));
        private IWebElement SearchButton => _driver.FindElement(By.XPath("//button[@id='search' and contains(text(),'Search')]"));
        private IWebElement CountryDropdown => _driver.FindElement(By.XPath("//select[@id='availableCountries']"));
        private IWebElement CityDropdown => _driver.FindElement(By.XPath("//select[@id='availableCities']"));
        private IWebElement? GetCheckboxBySkillName(string skillName) =>
            _driver.FindElements(By.XPath($"//input[@type='checkBox' and @name='skillsToSearch[]' and @value='{skillName}']"))
            .FirstOrDefault();

        private IWebElement? GetNextPageLink() =>
            _driver.FindElements(By.XPath("//ul[@class='pagination']//li[contains(@class,'active')]/following-sibling::li/a[@class='page-link']"))
            .FirstOrDefault();

        public SearchPage(IWebDriver driver) : base(driver)
        {
        }

        public void SelectSkill(string skillName)
        {
            while (true)
            {
                var checkbox = GetCheckboxBySkillName(skillName);

                if (checkbox != null)
                {
                    if (!checkbox.Selected)
                    {
                        _driver.ScrollAndJsClick(checkbox);
                    }
                    return;
                }

                var nextPageLink = GetNextPageLink();

                if (nextPageLink != null)
                {
                    nextPageLink.Click();
                }
                else
                {
                    throw new NoSuchElementException($"Checkbox for skill '{skillName}' not found.");
                }
            }
        }

        public void ClickSearch()
        {
            _driver.ScrollToElementAndClick(SearchButton);
        }

        public void OpenCountryDropdown()
        {
            CountryDropdown.Click();
        }

        public void SelectCountry(string countryName)
        {
            var select = new SelectElement(CountryDropdown);
            select.SelectByText(countryName);

            Retry.Until(() =>
            {
                var options = new SelectElement(CityDropdown).Options; 
                if (options.Count == 0) 
                    throw new Exception("City dropdown still empty");
            });
        }

        public void VerifyIsAtSearchPage()
        {
            _driver.WaitUntilUrlContains("/search");

            Retry.Until(() =>
            {
                if (!SearchButton.Displayed)
                    throw new RetryException("Search page not loaded yet.");
            });

            Assert.Multiple(() =>
            {
                Assert.That(CountryHeader.Displayed, "Country header is not visible.");
                Assert.That(CityHeader.Displayed, "City header is not visible.");
                Assert.That(SkillsHeader.Displayed, "Skills header is not visible.");
            });
        }

        public void VerifyCountryExists(string countryName)
        {
            var countrySelect = new SelectElement(CountryDropdown);
            var countries = countrySelect.Options.Select(o => o.Text.Trim()).ToList();

            Assert.That(countries, Does.Contain(countryName));
        }

        public void VerifyCityExists(string cityName)
        {
            var citySelect = new SelectElement(CityDropdown);
            var cities = citySelect.Options.Select(o => o.Text.Trim()).ToList();

            Assert.That(cities, Does.Contain(cityName));
        }
    }
}