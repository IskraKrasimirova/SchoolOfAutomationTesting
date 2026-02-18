using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using RestSharp;
using SeleniumTestFramework.ApiTests.Apis;
using SeleniumTestFramework.ApiTests.Models;
using SeleniumTestFramework.ApiTests.Models.Factories;
using SeleniumTestFramework.ApiTests.Utils;

namespace SeleniumTestFramework.ApiTests.Hooks
{
    public class DependencyContainer
    {
        [ScenarioDependencies]
        public static IServiceCollection RegisterDependencies()
        {
            var services = new ServiceCollection();

            services.AddSingleton(sp =>
            {
                return ConfigurationManager.Instance.SettingsModel;
            });

            services.AddSingleton<RestClient>(sp =>
            {
                var settings = sp.GetRequiredService<SettingsModel>();
                var options = new RestClientOptions(settings.BaseUrl);
                var client = new RestClient(options);
                client.AddDefaultHeader("Accept", "application/json");
                return client;
            });

            services.AddSingleton<IUserFactory, UserFactory>();

            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<RestClient>();
                return new UsersApi(client);
            });

            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<RestClient>();
                return new LoginApi(client);
            });

            return services;
        }
    }
}
