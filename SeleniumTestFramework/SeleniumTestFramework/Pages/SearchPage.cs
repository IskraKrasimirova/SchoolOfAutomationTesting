using OpenQA.Selenium;
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
    }
}