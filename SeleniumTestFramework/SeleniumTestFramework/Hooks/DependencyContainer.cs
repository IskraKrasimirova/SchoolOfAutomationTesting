using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTestFramework.Hooks
{
    public class DependencyContainer
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IWebDriver>(sp =>
            {
                new DriverManager().SetUpDriver(new ChromeConfig());

                var driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                return driver;
            });

            services.AddSingleton<SettingsModel>(sp =>
            {
                return ConfigurationManager.Instance.SettingsModel;
            });

            services.AddScoped<LoginPage>();
            services.AddScoped<DashboardPage>();
            services.AddScoped<RegisterPage>();
            services.AddScoped<UsersPage>();
            
            return services;
        }
    }
}
