using OpenQA.Selenium;
using SoAAutomationFramework.Utils;

namespace SoAAutomationFramework.Actions
{
    public class BaseUserActions
    {
        protected readonly IWebDriver _driver;

        public IWebDriver Driver => _driver;

        public BaseUserActions()
        {
            _driver = WebDriverProvider.GetPreparedDriver();
        }

        public void StartApplication()
        {
            OpenPage("Home");
        }

        public void OpenPage(string pageName)
        {
            string? baseUrl = ConfigurationReader.GetConfigurationProperty()[ConfigurationProperties.BASE_URL];

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                Console.WriteLine($"[ERROR] BASE_URL is missing in configuration.");
                throw new InvalidOperationException($"Missing or empty BASE_URL in configuration.");
            }

            string? path = ConfigurationReader.GetConfigurationProperty()[$"{ConfigurationProperties.PAGES_PATH}{pageName.ToLower()}"];
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine($"[ERROR] Missing path for page '{pageName}' in configuration.");
                throw new InvalidOperationException($"Missing path for page '{pageName}' in configuration.");
            } 
                
            string url = baseUrl.TrimEnd('/') + "/" + path.TrimStart('/');

            _driver.Navigate().GoToUrl(url);
        }
    }
}
