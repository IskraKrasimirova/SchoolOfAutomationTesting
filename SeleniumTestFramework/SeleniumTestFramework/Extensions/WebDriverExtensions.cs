using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTestFramework.Extensions
{
    public static class WebDriverExtensions
    {
        public static void EnterText(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public static void WaitUntilUrlContains(this IWebDriver driver, string expectedUrlPart, int timeoutInSeconds = 5)
        {
            driver.WaitForPredicate(timeoutInSeconds)
                .Until(ExpectedConditions.UrlContains(expectedUrlPart));
        }

        public static void WaitUntilElementIsVisible(this IWebDriver driver, IWebElement element, int timeoutInSeconds = 5)
        {
            driver.WaitForPredicate(timeoutInSeconds)
                .Until(d => element.Displayed);
        }

        public static void WaitUntilElementIsClickable(this IWebDriver driver, IWebElement element, int timeoutInSeconds = 5)
        {
            driver.WaitForPredicate(timeoutInSeconds)
                .Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public static void WaitUntilValueIsPresent(this IWebDriver driver, IWebElement element, string value, int timeoutInSeconds = 5)
        {
            driver.WaitForPredicate(timeoutInSeconds)
                .Until(ExpectedConditions.TextToBePresentInElementValue(element, value));
        }

        public static void WaitUntilTextIsPresent(this IWebDriver driver, IWebElement element, string expectedText, int timeoutInSeconds = 5) 
        { 
            driver.WaitForPredicate(timeoutInSeconds)
                .Until(ExpectedConditions.TextToBePresentInElement(element, expectedText)); 
        }

        private static WebDriverWait WaitForPredicate(this IWebDriver driver, int timeoutInSeconds = 10)
        {
            var customWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            customWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));

            return customWait;
        }
    }
}
