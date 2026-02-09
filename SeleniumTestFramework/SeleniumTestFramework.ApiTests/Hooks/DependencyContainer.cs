using Microsoft.Extensions.DependencyInjection;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using RestSharp;
using SeleniumTestFramework.ApiTests.Apis;

namespace SeleniumTestFramework.ApiTests.Hooks
{
    public class DependencyContainer
    {
        [ScenarioDependencies]
        public static IServiceCollection RegisterDependencies()
        {
            var services = new ServiceCollection();
            services.AddSingleton<RestClient>(sp =>
            {
                //TODO - Move the base URL to a configuration file
                var options = new RestClientOptions("http://localhost:5000");
                var client = new RestClient(options);
                client.AddDefaultHeader("Accept", "application/json");
                return client;
            });

            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<RestClient>();
                return new UsersApi(client);
            });

            return services;
        }
    }
}
