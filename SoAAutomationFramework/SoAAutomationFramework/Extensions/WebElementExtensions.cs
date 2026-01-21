using OpenQA.Selenium;

namespace SoAAutomationFramework.Extensions
{
    public static class WebElementExtensions
    {
        public static void EnterText(this IWebElement element, string text) 
        { 
            element.Clear(); 
            element.SendKeys(text);
        }
    }
}
