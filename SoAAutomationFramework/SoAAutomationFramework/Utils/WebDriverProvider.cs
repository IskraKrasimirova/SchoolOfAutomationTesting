using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Runtime.CompilerServices;
using static SoAAutomationFramework.Utils.ConfigurationProperties;

namespace SoAAutomationFramework.Utils
{
    internal class WebDriverProvider
    {
        private const string CHROME_DRIVER = "chrome";
        private const string FIREFOX_DRIVER = "firefox";
        private static readonly IConfigurationRoot config = ConfigurationReader.GetConfigurationProperty();

        private static IWebDriver? _driver;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IWebDriver GetPreparedDriver()
        {
            if (_driver == null)
                throw new InvalidOperationException("WebDriver is not initialized. Call InitDriver() first.");

            return _driver;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void InitDriver()
        {
            string? desireDriver = config[DRIVER]?.ToLower();

            if (string.IsNullOrWhiteSpace(desireDriver))
            {
                throw new InvalidOperationException("Missing 'webdriver:driver' in config.");
            }

            switch (desireDriver)
            {
                case CHROME_DRIVER: CreateChromeDriver(); break;
                case FIREFOX_DRIVER: CreateFireFoxDriver(); break;
                default:
                    throw new NotSupportedException($"The specified driver '{desireDriver}' is not supported.");
            }

            if (_driver == null)
                throw new InvalidOperationException("WebDriver creation failed.");

            string? implicitWait = config[DRIVER_IMPLICIT_WAIT];
            if (!double.TryParse(implicitWait, out double implicitTimeout))
            {
                implicitTimeout = 5000;
            }

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(implicitTimeout);
        }

        private static void CreateChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            string? browserMode = config[BROWSER_WINDOW_MODE];

            if ("maximized".Equals(browserMode?.ToLower()))
            {
                options.AddArguments("start-maximized");
            }

            _driver = new ChromeDriver(options);
        }

        private static void CreateFireFoxDriver()
        {
            _driver = new FirefoxDriver();
        }
    }
}
