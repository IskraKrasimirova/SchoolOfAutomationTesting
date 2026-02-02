using OpenQA.Selenium;

namespace SeleniumTestFramework.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver _driver;

        protected IWebElement BaseElement => _driver.FindElement(By.TagName("body"));

        protected BasePage(IWebDriver driver)
        {
            this._driver = driver;
        }

        public void VerifyPageIsLoaded()
        {
            Assert.That(BaseElement.Displayed, Is.True);
        }
    }
}
