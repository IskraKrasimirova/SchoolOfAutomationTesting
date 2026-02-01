using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using SeleniumTestFramework.DatabaseOperations.Operations;
using SeleniumTestFramework.Models;
using SeleniumTestFramework.Pages;
using SeleniumTestFramework.Utilities;
using System.Data;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumTestFramework.Hooks
{
    public class DependencyContainer
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();
            
            services.AddSingleton<SettingsModel>(sp =>
            {
                return ConfigurationManager.Instance.SettingsModel;
            });

            services.AddScoped<IWebDriver>(sp =>
            {
                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

                var driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

                return driver;
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new LoginPage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new RegisterPage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new DashboardPage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new UsersPage(driver);
            });

            services.AddScoped(sp =>
            {
                var driver = sp.GetRequiredService<IWebDriver>();
                return new AddUserModalPage(driver);
            });

            // Short syntax for registering page classes
            //services.AddScoped<LoginPage>();
            //services.AddScoped<DashboardPage>();
            //services.AddScoped<RegisterPage>();
            //services.AddScoped<UsersPage>();

            services.AddScoped<IDbConnection>(sp =>
            {
                var settings = sp.GetRequiredService<SettingsModel>();
                var connectionString = settings.ConnectionString;

                var dbConnection = new MySqlConnection(connectionString);
                return dbConnection;
            });

            services.AddScoped(sp =>
            {
                var dbConnection = sp.GetRequiredService<IDbConnection>();
                return new UserOperations(dbConnection);
            });

            return services;
        }
    }
}
